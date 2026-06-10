using System;
using System.IO;
using System.Windows.Media;

namespace Kingdoms.Bot
{
    // Plays user-chosen alert sounds for radar notifications.
    // Init() must run on the UI thread; Play/Stop are safe from any thread
    // because they marshal through the MediaPlayer's dispatcher.
    public static class RadarSoundPlayer
    {
        private static MediaPlayer _player;

        public static void Init()
        {
            if (_player == null)
                _player = new MediaPlayer();
        }

        public static bool IsSoundFileValid(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return false;
            string ext = Path.GetExtension(path).ToLowerInvariant();
            return ext == ".mp3" || ext == ".wav" || ext == ".wma";
        }

        public static void Play(string path)
        {
            if (_player == null || !IsSoundFileValid(path))
                return;
            try
            {
                _player.Dispatcher.BeginInvoke(new Action(delegate
                {
                    _player.Open(new Uri(path, UriKind.Absolute));
                    _player.Play();
                }));
            }
            catch (Exception ex)
            {
                BotLogger.Log("Radar", BotLogLevel.Error, "Sound notification failed: " + ex.Message);
            }
        }

        public static void Stop()
        {
            if (_player == null)
                return;
            try
            {
                _player.Dispatcher.BeginInvoke(new Action(delegate { _player.Stop(); }));
            }
            catch (Exception ex)
            {
                BotLogger.Log("Radar", BotLogLevel.Error, "Sound stop failed: " + ex.Message);
            }
        }
    }
}
