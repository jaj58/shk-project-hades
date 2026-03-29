using System;
using System.IO;
using System.Net;
using System.Text;

namespace Kingdoms.Bot
{
    public static class DiscordNotifier
    {
        public static void SendWebhook(string webhookUrl, string title, string message, int color)
        {
            if (string.IsNullOrEmpty(webhookUrl))
                return;

            try
            {
                string escapedTitle = EscapeJson(title);
                string escapedMessage = EscapeJson(message);
                string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

                string json = "{\"embeds\":[{" +
                    "\"title\":\"" + escapedTitle + "\"," +
                    "\"description\":\"" + escapedMessage + "\"," +
                    "\"color\":" + color + "," +
                    "\"timestamp\":\"" + timestamp + "\"," +
                    "\"footer\":{\"text\":\"Project Hades Radar\"}" +
                    "}]}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webhookUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = 5000;

                byte[] data = Encoding.UTF8.GetBytes(json);
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Discord returns 204 No Content on success
                }
            }
            catch (Exception ex)
            {
                BotLogger.Log("Discord", BotLogLevel.Warning, "Webhook failed: " + ex.Message);
            }
        }

        public static void SendAsync(string webhookUrl, string title, string message, int color)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                SendWebhook(webhookUrl, title, message, color);
            });
        }

        private static string EscapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n")
                    .Replace("\r", "\\r")
                    .Replace("\t", "\\t");
        }
    }
}
