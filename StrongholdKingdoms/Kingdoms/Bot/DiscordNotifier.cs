using System;
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
                // Discord requires TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

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

                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    client.UploadString(webhookUrl, json);
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
