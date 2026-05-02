using System;
using System.Net;
using System.Text;

namespace Kingdoms.Bot
{
    public static class DiscordNotifier
    {
        public static void SendWebhook(string webhookUrl, string title, string message, int color, string mention = null)
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

                // "content" is plain text above the embed — the only field Discord
                // resolves mentions in. Put the tag here so it fires a real ping.
                string contentField = string.IsNullOrEmpty(mention)
                    ? ""
                    : "\"content\":\"" + EscapeJson(mention) + "\",";

                string json = "{" + contentField + "\"embeds\":[{" +
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

        public static void SendAsync(string webhookUrl, string title, string message, int color, string mention = null)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                SendWebhook(webhookUrl, title, message, color, mention);
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
