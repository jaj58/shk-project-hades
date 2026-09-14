// Banquet Sender — group website. Talks to ../banquet_api.php with the shared group key.
// Every game-supplied string (player / village names, errors) is inserted as text, never HTML.
(function () {
  'use strict';

  var API = '../banquet_api.php';
  var REFRESH_MS = 10000;
  var STORAGE_KEY = 'bq_group_key';

  var key = null;
  var state = null;        // last get_state response
  var form = null;         // working copy of settings (what the page shows)
  var dirty = false;
  var fetchedAt = 0;
  var shipTab = 'active';
  var refreshTimer = null;

  var STATUS = {
    leased:    ['Queued', 'amber'],
    in_flight: ['On the way', 'blue'],
    settled:   ['Delivered', 'green'],
    failed:    ['Rejected', 'red'],
    cancelled: ['Cancelled', ''],
    expired:   ['Expired', '']
  };

  var SKIP_TEXT = {
    'produces it': 'makes it',
    'not researched': 'not researched',
    'no village hall': 'no hall',
    'not focused': '',
    'paused': 'paused',
    'report too old': 'old report',
    'good disabled': 'off',
    'no player': '',
    'off': ''
  };

  var MAIN_SETTINGS = [
    ['keep_amount', 'Keep amount', 'Givers never go below this many of a good (balancing too)'],
    ['safety_margin', 'Safety margin', 'Fill to this many under the target'],
    ['min_send', 'Smallest shipment', 'Orders below this aren\'t created'],
    ['max_travel_minutes', 'Max travel (minutes)', '0 = any distance']
  ];
  var ADV_SETTINGS = [
    ['max_merchants_per_order', 'Merchants per order', 'Largest single shipment in merchants'],
    ['max_orders_per_player', 'Open orders per player', 'Queued orders one player works through'],
    ['settle_buffer_seconds', 'Arrival buffer (s)', 'Keep counting a delivery until the receiver\'s data is this much newer than its arrival'],
    ['lease_seconds', 'Order lease (s)', 'An unsent order is re-planned after this long'],
    ['producer_min_buildings', 'Producer buildings', 'Auto-fill: buildings needed to count as making a good'],
    ['balance_tolerance_percent', 'Balance tolerance (%)', 'Only rebalance a village more than this % of its cap off the balance level'],
    ['max_report_age_hours', 'Max report age (h)', 'Skip receivers not seen for this long (0 = never)']
  ];
  var TOGGLE_SETTINGS = [
    ['balance_goods', 'Balance goods between villages that make them (Auto-fill: same fill % for every producer, after non-producers are served)'],
    ['include_own_villages', 'Send between a player\'s own villages'],
    ['send_unusable_goods', 'Send goods the receiver can\'t banquet with yet (not researched — still stored)']
  ];

  // ── DOM helpers ────────────────────────────────────────────────────────────

  function $(id) { return document.getElementById(id); }

  function el(tag, attrs, children) {
    var n = document.createElement(tag);
    if (attrs) {
      for (var k in attrs) {
        if (!attrs.hasOwnProperty(k) || attrs[k] == null) continue;
        if (k === 'class') n.className = attrs[k];
        else if (k === 'text') n.textContent = attrs[k];
        else if (k.slice(0, 2) === 'on') n.addEventListener(k.slice(2), attrs[k]);
        else if (k === 'checked' || k === 'value' || k === 'disabled') n[k] = attrs[k];
        else n.setAttribute(k, attrs[k]);
      }
    }
    (children || []).forEach(function (c) {
      if (c == null || c === false) return;
      n.appendChild(typeof c === 'string' || typeof c === 'number' ? document.createTextNode(String(c)) : c);
    });
    return n;
  }

  function clear(node) { while (node.firstChild) node.removeChild(node.firstChild); }
  function fmt(n) { return Math.round(n || 0).toLocaleString('en-GB'); }
  function clone(o) { return JSON.parse(JSON.stringify(o)); }

  function duration(sec) {
    sec = Math.max(0, Math.round(sec));
    var h = Math.floor(sec / 3600), m = Math.floor(sec % 3600 / 60), s = sec % 60;
    if (h > 0) return h + 'h ' + (m < 10 ? '0' : '') + m + 'm';
    if (m > 0) return m + 'm ' + (s < 10 ? '0' : '') + s + 's';
    return s + 's';
  }

  function toast(msg, isError) {
    var t = $('toast');
    t.textContent = msg;
    t.className = 'toast' + (isError ? ' err' : '');
    clearTimeout(toast.timer);
    toast.timer = setTimeout(function () { t.className = 'toast hidden'; }, isError ? 7000 : 3000);
  }

  // ── API ────────────────────────────────────────────────────────────────────

  function api(action, body) {
    body = body || {};
    body.action = action;
    body.key = key;
    return fetch(API, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
      cache: 'no-store'
    }).then(function (r) {
      return r.json().catch(function () { throw new Error('HTTP ' + r.status); });
    }).then(function (j) {
      if (!j.ok) throw new Error(j.error || 'Request failed');
      return j;
    });
  }

  // ── Key handling ───────────────────────────────────────────────────────────

  function readStoredKey() {
    try { return localStorage.getItem(STORAGE_KEY); } catch (e) { return null; }
  }
  function storeKey(k) {
    try { if (k) localStorage.setItem(STORAGE_KEY, k); else localStorage.removeItem(STORAGE_KEY); } catch (e) {}
  }

  function start() {
    var m = /[#&]key=([^&]+)/.exec(location.hash);
    if (m) {
      var k = decodeURIComponent(m[1]);
      // Drop the key from the address bar and history.
      history.replaceState(null, '', location.pathname + location.search);
      openGroup(k, true);
      return;
    }
    var stored = readStoredKey();
    if (stored) openGroup(stored, true);
    else showLogin();
  }

  function showLogin(error) {
    stopRefresh();
    $('app').classList.add('hidden');
    $('login').classList.remove('hidden');
    $('login-error').textContent = error || '';
    $('login-error').classList.toggle('hidden', !error);
    $('login-key').focus();
  }

  function openGroup(k, remember) {
    key = k;
    dirty = false;
    api('get_state').then(function (j) {
      if (remember) storeKey(k);
      $('login').classList.add('hidden');
      $('app').classList.remove('hidden');
      applyState(j);
      startRefresh();
    }).catch(function (e) {
      storeKey(null);
      showLogin(e.message);
    });
  }

  // ── Refresh loop ───────────────────────────────────────────────────────────

  function startRefresh() {
    stopRefresh();
    refreshTimer = setInterval(refresh, REFRESH_MS);
  }
  function stopRefresh() {
    if (refreshTimer) clearInterval(refreshTimer);
    refreshTimer = null;
  }
  function refresh() {
    if (document.hidden) return;
    api('get_state').then(applyState).catch(function (e) { toast('Refresh failed: ' + e.message, true); });
  }

  function applyState(j) {
    state = j;
    fetchedAt = Date.now();
    if (!dirty) form = clone(j.settings);
    render();
  }

  function markDirty() {
    dirty = true;
    $('savebar').classList.remove('hidden');
  }

  function save() {
    $('save-btn').disabled = true;
    api('set_settings', { settings: form, cancel_open_orders: true }).then(function (j) {
      dirty = false;
      form = clone(j.settings);
      $('savebar').classList.add('hidden');
      toast('Settings saved.');
      refresh();
    }).catch(function (e) {
      toast('Save failed: ' + e.message, true);
    }).then(function () { $('save-btn').disabled = false; });
  }

  function discard() {
    dirty = false;
    form = clone(state.settings);
    $('savebar').classList.add('hidden');
    render();
  }

  // ── Lookups ────────────────────────────────────────────────────────────────

  function playerById(id) {
    for (var i = 0; i < state.players.length; i++) if (state.players[i].user_id === id) return state.players[i];
    return null;
  }
  function villageById(id) {
    for (var i = 0; i < state.villages.length; i++) if (state.villages[i].village_id === id) return state.villages[i];
    return null;
  }
  function villageLabel(id) {
    var v = villageById(id);
    if (!v) return 'Village ' + id;
    var p = playerById(v.user_id);
    return v.name + (p ? ' (' + p.name + ')' : '');
  }
  function capsOf(which) {
    var c = form[which];
    if (!c || Array.isArray(c)) { c = {}; form[which] = c; }
    return c;
  }

  // ── Render ─────────────────────────────────────────────────────────────────

  function render() {
    if (!state || !form) return;
    renderHeader();
    $('new-group').classList.toggle('hidden', state.exists && state.players.length > 0);
    renderModes();
    renderGoods();
    renderFocus();
    renderSettings();
    renderPlayers();
    renderGrid();
    renderShipments();
  }

  function renderHeader() {
    var online = (state.players || []).filter(function (p) { return p.online; }).length;
    var active = state.active || [];
    var goods = 0, flying = 0;
    active.forEach(function (s) { if (s.status === 'in_flight') { flying++; goods += s.amount; } });
    $('stat-online').textContent = online;
    $('stat-flight').textContent = flying;
    $('stat-goods').textContent = fmt(goods);
    $('stat-updated').textContent = 'updated ' + new Date(fetchedAt).toLocaleTimeString();

    var mode = state.settings.mode;
    var pill = $('mode-pill');
    pill.textContent = mode === 'autofill' ? 'Auto-fill' : mode === 'focus' ? 'Focus' : 'Off';
    pill.className = 'pill ' + (mode === 'off' ? 'red' : 'gold');
  }

  function renderModes() {
    var box = $('modes');
    clear(box);
    [
      ['off', 'Off', 'Nothing is sent. Players still report.'],
      ['autofill', 'Auto-fill every village', 'Each village is topped up with the goods it doesn\'t make, from villages that make them.'],
      ['focus', 'Focus players / villages', 'Everyone fills the chosen players or villages with every good.']
    ].forEach(function (m) {
      box.appendChild(el('button', {
        class: 'mode' + (form.mode === m[0] ? ' active' : ''),
        onclick: function () { form.mode = m[0]; markDirty(); render(); }
      }, [el('b', { text: m[1] }), el('small', { text: m[2] })]));
    });
  }

  function renderGoods() {
    var box = $('goods');
    clear(box);
    state.goods.forEach(function (name, g) {
      box.appendChild(el('span', {
        class: 'good-toggle' + (form.goods_enabled[g] ? ' on' : ''),
        text: name,
        onclick: function () { form.goods_enabled[g] = !form.goods_enabled[g]; markDirty(); render(); }
      }));
    });
  }

  function renderFocus() {
    $('focus-box').classList.toggle('hidden', form.mode !== 'focus');
    var box = $('focus-players');
    clear(box);
    if (!state.players.length) { box.appendChild(el('span', { class: 'muted', text: 'No players yet.' })); return; }
    state.players.forEach(function (p) {
      var on = form.focus_players.indexOf(p.user_id) >= 0;
      box.appendChild(el('label', { class: 'check' }, [
        el('input', { type: 'checkbox', checked: on, onchange: function () { toggleIn(form.focus_players, p.user_id); markDirty(); render(); } }),
        p.name
      ]));
    });
    var villages = form.focus_villages.map(villageLabel);
    if (villages.length) box.appendChild(el('span', { class: 'muted', text: 'Villages: ' + villages.join(', ') }));
  }

  function toggleIn(list, id) {
    var i = list.indexOf(id);
    if (i >= 0) list.splice(i, 1); else list.push(id);
  }

  function numberSetting(def) {
    return el('div', { class: 'setting' }, [
      el('div', {}, [el('span', { class: 'label', text: def[1] }), el('span', { class: 'hint', text: def[2] })]),
      el('input', {
        type: 'number', min: '0', value: form[def[0]],
        oninput: function (e) { form[def[0]] = parseInt(e.target.value, 10) || 0; markDirty(); }
      })
    ]);
  }

  function renderSettings() {
    var main = $('settings-main'), adv = $('settings-adv');
    // Don't rebuild inputs the user is typing in.
    if (main.contains(document.activeElement) || adv.contains(document.activeElement)) return;
    clear(main);
    clear(adv);
    MAIN_SETTINGS.forEach(function (d) { main.appendChild(numberSetting(d)); });
    TOGGLE_SETTINGS.forEach(function (d) {
      main.appendChild(el('div', { class: 'setting' }, [
        el('label', { class: 'check' }, [
          el('input', { type: 'checkbox', checked: !!form[d[0]], onchange: function (e) { form[d[0]] = e.target.checked; markDirty(); } }),
          d[1]
        ])
      ]));
    });
    ADV_SETTINGS.forEach(function (d) { adv.appendChild(numberSetting(d)); });
  }

  function renderPlayers() {
    var table = $('players-table');
    if (table.contains(document.activeElement)) return;
    clear(table);
    if (!state.players.length) {
      table.appendChild(el('tr', {}, [el('td', { class: 'empty', text: 'No players have connected with this key yet.' })]));
      return;
    }
    table.appendChild(el('tr', {}, ['Player', 'World', 'Villages', 'Goods unlocked', 'Merchant speed', 'Hall card',
      'Max per player', 'Paused', 'Version', ''].map(function (h) { return el('th', { text: h }); })));

    var caps = capsOf('player_caps');
    state.players.forEach(function (p) {
      var seen = p.online ? 'online' : (p.last_seen_ago == null ? 'offline' : 'seen ' + duration(p.last_seen_ago) + ' ago');
      var cards = p.cards || {};
      var paused = form.paused_players.indexOf(p.user_id) >= 0;
      table.appendChild(el('tr', {}, [
        el('td', {}, [el('span', { class: 'dot' + (p.online ? ' on' : '') }), el('b', { text: p.name || ('User ' + p.user_id) }),
          el('div', { class: 'faint', style: 'font-size:12px', text: seen })]),
        el('td', { text: p.world || '—' }),
        el('td', { class: 'num', text: p.villages }),
        el('td', { class: 'num', text: p.craftsmanship + ' / 8' }),
        el('td', { class: 'num', text: cards.merchant_speed ? 'x' + cards.merchant_speed : '—' }),
        el('td', {}, [cards.hall_multiplier > 1 ? el('span', { class: 'pill gold', text: 'x' + cards.hall_multiplier }) : el('span', { class: 'faint', text: 'no' })]),
        el('td', {}, [el('input', {
          type: 'number', class: 'cap', min: '0', placeholder: 'hall cap', value: caps[p.user_id] || '',
          oninput: function (e) {
            var v = parseInt(e.target.value, 10);
            if (v > 0) caps[p.user_id] = v; else delete caps[p.user_id];
            markDirty();
          }
        })]),
        el('td', {}, [el('input', { type: 'checkbox', checked: paused, onchange: function () { toggleIn(form.paused_players, p.user_id); markDirty(); } })]),
        el('td', { class: 'faint', text: p.client_version || '' }),
        el('td', {}, [el('button', {
          class: 'btn small danger', text: 'Remove',
          onclick: function () {
            if (!confirm('Remove ' + p.name + ' and their villages from the group? If their bot is still enabled they will rejoin on its next sync.')) return;
            api('remove_player', { user_id: p.user_id }).then(function () { toast('Player removed.'); refresh(); })
              .catch(function (e) { toast(e.message, true); });
          }
        })])
      ]));
    });
  }

  function renderGrid() {
    var table = $('grid');
    if (table.contains(document.activeElement)) return;
    clear(table);
    if (!state.villages.length) {
      table.appendChild(el('tr', {}, [el('td', { class: 'empty', text: 'No villages reported yet.' })]));
      return;
    }
    var head = [el('th', { text: 'Village' })];
    state.goods.forEach(function (g) { head.push(el('th', { class: 'good', text: g })); });
    table.appendChild(el('tr', {}, head));

    var vcaps = capsOf('village_caps');
    var byPlayer = {};
    state.villages.forEach(function (v) { (byPlayer[v.user_id] = byPlayer[v.user_id] || []).push(v); });
    var playerIds = Object.keys(byPlayer).map(Number).sort(function (a, b) {
      var pa = playerById(a), pb = playerById(b);
      return (pa ? pa.name : '').localeCompare(pb ? pb.name : '');
    });

    playerIds.forEach(function (uid) {
      var p = playerById(uid);
      table.appendChild(el('tr', { class: 'player-row' }, [el('td', { colspan: String(state.goods.length + 1), text: p ? p.name : 'User ' + uid })]));
      byPlayer[uid].sort(function (a, b) { return a.name.localeCompare(b.name); }).forEach(function (v) {
        var focused = form.focus_villages.indexOf(v.village_id) >= 0;
        var meta = 'cap ' + fmt(v.effective_cap) + (v.effective_cap !== v.hall_cap ? ' of ' + fmt(v.hall_cap) : '') +
          ' · ' + v.merchants_free + ' merchants';
        var age = v.snapshot_age_sec == null ? 'never downloaded' : 'data ' + duration(v.snapshot_age_sec) + ' old';
        var row = [el('td', {}, [
          el('div', {}, [
            el('button', {
              class: 'btn small', title: 'Focus this village', text: focused ? '★' : '☆',
              style: focused ? 'color:var(--gold)' : '',
              onclick: function () { toggleIn(form.focus_villages, v.village_id); markDirty(); render(); }
            }), ' ',
            el('span', { class: 'vname', text: v.name }),
            v.has_hall ? null : el('span', { class: 'pill red', style: 'margin-left:6px', text: 'no hall' })
          ]),
          el('div', { class: 'vmeta', text: meta }),
          el('div', { class: 'vmeta' }, [age, ' · max ', el('input', {
            type: 'number', class: 'cap', min: '0', placeholder: 'auto', value: vcaps[v.village_id] || '',
            oninput: function (e) {
              var n = parseInt(e.target.value, 10);
              if (n > 0) vcaps[v.village_id] = n; else delete vcaps[v.village_id];
              markDirty();
            }
          })])
        ])];

        var cells = (state.grid[String(v.village_id)] || []);
        state.goods.forEach(function (name, g) { row.push(gridCell(v, g, cells[g])); });
        table.appendChild(el('tr', {}, row));
      });
    });
  }

  function gridCell(v, g, c) {
    c = c || { level: 0, inbound: 0, leased_in: 0, target: 0, shortfall: 0, skip: 'off' };
    var cap = Math.max(v.hall_cap, 1);
    var pct = function (n) { return Math.max(0, Math.min(100, n / cap * 100)); };
    var levelPct = pct(c.level), inboundPct = pct(c.inbound), leasedPct = pct(c.leased_in);
    var produces = v.buildings && v.buildings[g] > 0;
    var skipText = SKIP_TEXT.hasOwnProperty(c.skip) ? SKIP_TEXT[c.skip] : c.skip;
    var title = name(g) + ': ' + fmt(c.level) + ' in hall';
    if (c.inbound) title += ', ' + fmt(c.inbound) + ' on the way';
    if (c.leased_in) title += ', ' + fmt(c.leased_in) + ' queued';
    if (c.balance) {
      // A producer evened out with the group's other producers of this good.
      skipText = 'balance to ' + fmt(c.target);
      title += '. Balancing with other producers to ' + fmt(c.target) +
        (c.shortfall ? ' (' + fmt(c.shortfall) + ' short)' : '');
    } else if (c.target) {
      title += '. Filling to ' + fmt(c.target) + (c.shortfall ? ' (' + fmt(c.shortfall) + ' short)' : '');
    }
    if (c.skip) title += '. Not receiving: ' + c.skip;
    if (produces) title += '. ' + v.buildings[g] + ' building(s), ' + fmt(v.prod[g]) + '/day';

    var extra = [];
    if (c.inbound) extra.push('+' + fmt(c.inbound));
    if (c.leased_in) extra.push('+' + fmt(c.leased_in) + 'q');

    return el('td', { class: 'cell' + (c.target > 0 ? '' : ' skip'), title: title }, [
      el('div', { class: 'cell-top' }, [
        el('span', { class: 'lvl num', text: fmt(c.level) }),
        produces ? el('span', { class: 'prod', text: '●' }) : null,
        el('span', { class: 'num muted', text: extra.join(' ') })
      ]),
      el('div', { class: 'bar' }, [
        el('i', { class: 'level', style: 'width:' + levelPct + '%' }),
        el('i', { class: 'inbound', style: 'left:' + levelPct + '%;width:' + inboundPct + '%' }),
        el('i', { class: 'leased', style: 'left:' + Math.min(100, levelPct + inboundPct) + '%;width:' + leasedPct + '%' })
      ]),
      skipText ? el('div', { class: 'skip-reason', text: skipText }) : null
    ]);
  }

  function name(g) { return state.goods[g] || ('Good ' + g); }

  function renderShipments() {
    var table = $('ship-table');
    clear(table);
    var rows = shipTab === 'active' ? state.active : state.recent;
    $('clear-all-btn').classList.toggle('hidden', shipTab !== 'active' || !state.active.length);
    if (!rows.length) {
      table.appendChild(el('tr', {}, [el('td', { class: 'empty', text: shipTab === 'active' ? 'Nothing queued or on the way.' : 'No finished shipments yet.' })]));
      return;
    }
    var elapsed = (Date.now() - fetchedAt) / 1000;
    table.appendChild(el('tr', {}, ['Status', 'Good', 'Amount', 'From', 'To', shipTab === 'active' ? 'Arrives' : 'When', '', ''].map(function (h) { return el('th', { text: h }); })));

    rows.slice().sort(function (a, b) {
      if (shipTab !== 'active') return a.updated_ago - b.updated_ago;
      return (a.eta_in_sec == null ? 1e12 : a.eta_in_sec) - (b.eta_in_sec == null ? 1e12 : b.eta_in_sec);
    }).forEach(function (s) {
      var st = STATUS[s.status] || [s.status, ''];
      var label = st[0], color = st[1];
      var when = '';
      if (shipTab === 'active') {
        if (s.eta_in_sec != null) {
          var left = s.eta_in_sec - elapsed;
          if (left > 0) when = 'in ' + duration(left);
          else {
            when = 'arrived ' + duration(-left) + ' ago';
            label = 'Arrived';
            color = 'green';
          }
        } else if (s.status === 'leased') when = 'waiting for sender';
      } else {
        when = duration(s.updated_ago + elapsed) + ' ago';
      }
      table.appendChild(el('tr', {}, [
        el('td', {}, [el('span', { class: 'pill ' + color, text: label })]),
        el('td', { text: name(s.good) }),
        el('td', { class: 'num', text: fmt(s.amount) }),
        el('td', { text: villageLabel(s.from_village_id) }),
        el('td', { text: villageLabel(s.to_village_id) }),
        el('td', { class: 'num', 'data-eta': shipTab === 'active' && s.eta_in_sec != null ? String(s.eta_in_sec) : null, text: when }),
        el('td', { class: 'faint', text: (s.source === 'adopted' ? 'sent outside the planner' : '') + (s.error ? (s.source === 'adopted' ? ' · ' : '') + s.error : '') }),
        el('td', {}, [shipTab === 'active' ? el('button', {
          class: 'btn small', text: 'Stop counting',
          title: 'Stop counting these goods at the receiver (e.g. the shipment never really left)',
          onclick: function () {
            api('clear_shipments', { ids: [s.id] }).then(function () { refresh(); }).catch(function (e) { toast(e.message, true); });
          }
        }) : null])
      ]));
    });
  }

  // ── Wiring ─────────────────────────────────────────────────────────────────

  $('login-btn').addEventListener('click', function () {
    var k = $('login-key').value.trim();
    if (k.length < 6) { showLogin('The key must be at least 6 characters.'); return; }
    openGroup(k, $('login-remember').checked);
  });
  $('login-key').addEventListener('keydown', function (e) { if (e.key === 'Enter') $('login-btn').click(); });
  $('logout-btn').addEventListener('click', function () { storeKey(null); key = null; state = null; showLogin(); });
  $('save-btn').addEventListener('click', save);
  $('discard-btn').addEventListener('click', discard);
  $('clear-all-btn').addEventListener('click', function () {
    if (!confirm('Stop counting every queued and on-the-way shipment? Receivers may then be sent more before their data refreshes — only do this if the ledger is wrong.')) return;
    api('clear_shipments', { all: true }).then(function (j) { toast(j.cleared + ' shipment(s) cleared.'); refresh(); })
      .catch(function (e) { toast(e.message, true); });
  });
  Array.prototype.forEach.call(document.querySelectorAll('.tab'), function (b) {
    b.addEventListener('click', function () {
      shipTab = b.getAttribute('data-tab');
      Array.prototype.forEach.call(document.querySelectorAll('.tab'), function (x) { x.classList.toggle('active', x === b); });
      renderShipments();
    });
  });
  // Tick ETA text between refreshes without rebuilding rows (a rebuild under the
  // cursor would swallow clicks on the row buttons).
  setInterval(function () {
    if (!state || document.hidden) return;
    var elapsed = (Date.now() - fetchedAt) / 1000;
    Array.prototype.forEach.call(document.querySelectorAll('#ship-table td[data-eta]'), function (td) {
      var left = parseFloat(td.getAttribute('data-eta')) - elapsed;
      td.textContent = left > 0 ? 'in ' + duration(left) : 'arrived ' + duration(-left) + ' ago';
    });
  }, 1000);
  document.addEventListener('visibilitychange', function () { if (!document.hidden && key && state) refresh(); });
  window.addEventListener('beforeunload', function (e) { if (dirty) { e.preventDefault(); e.returnValue = ''; } });

  start();
})();
