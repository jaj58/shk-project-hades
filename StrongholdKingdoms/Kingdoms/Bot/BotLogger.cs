using System;
using System.Collections.Generic;

namespace Kingdoms.Bot
{
    public enum BotLogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }

    public class BotLogEntry
    {
        public DateTime Timestamp;
        public string ModuleName;
        public BotLogLevel Level;
        public string Message;
    }

    public static class BotLogger
    {
        private static readonly object _lock = new object();
        private static readonly List<BotLogEntry> _entries = new List<BotLogEntry>();
        private static int _maxEntries = 5000;

        public delegate void LogAddedHandler(BotLogEntry entry);
        public static event LogAddedHandler OnLogAdded;

        public static List<BotLogEntry> GetEntries()
        {
            lock (_lock)
            {
                return new List<BotLogEntry>(_entries);
            }
        }

        public static void Log(string moduleName, BotLogLevel level, string message)
        {
            BotLogEntry entry = new BotLogEntry
            {
                Timestamp = DateTime.Now,
                ModuleName = moduleName,
                Level = level,
                Message = message
            };

            lock (_lock)
            {
                _entries.Add(entry);
                if (_entries.Count > _maxEntries)
                    _entries.RemoveAt(0);
            }

            try
            {
                if (OnLogAdded != null)
                    OnLogAdded(entry);
            }
            catch
            {
            }
        }

        public static void Clear()
        {
            lock (_lock)
            {
                _entries.Clear();
            }
        }
    }
}
