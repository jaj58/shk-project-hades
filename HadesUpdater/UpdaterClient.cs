using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web.Script.Serialization;

namespace HadesUpdater
{
    public class VersionInfo
    {
        public string Version     { get; set; }
        public string DownloadUrl { get; set; }
        public string Changelog   { get; set; }
        public string ZipPassword { get; set; }
    }

    public class UpdaterClient
    {
        // TODO: replace with your real domain before deploying
        private const string ApiUrl = "https://www.projecthades.co.uk/api/updater_api.php";

        private readonly JavaScriptSerializer _json = new JavaScriptSerializer();

        // ------------------------------------------------------------------ //
        //  Public API                                                          //
        // ------------------------------------------------------------------ //

        /// <summary>
        /// Validates a license key + HWID against the server.
        /// On first use this binds the key to the machine.
        /// Throws on network error; returns false on auth failure.
        /// </summary>
        public bool ValidateLicense(string key, string hwid, out string message)
        {
            string response = Post(new NameValueCollection
            {
                { "action", "validate_license" },
                { "key",    key  },
                { "hwid",   hwid },
            });

            var dict = _json.Deserialize<System.Collections.Generic.Dictionary<string, object>>(response);
            message = dict.ContainsKey("message") ? dict["message"]?.ToString() : string.Empty;

            return dict.ContainsKey("valid") && (bool)dict["valid"];
        }

        /// <summary>
        /// Fetches the latest version info from the server.
        /// Requires a valid (non-expired, active) license key.
        /// </summary>
        public VersionInfo GetLatestVersion(string key)
        {
            string response = Post(new NameValueCollection
            {
                { "action", "latest_version" },
                { "key",    key },
            });

            var dict = _json.Deserialize<System.Collections.Generic.Dictionary<string, object>>(response);

            if (!dict.ContainsKey("ok") || !(bool)dict["ok"])
            {
                string error = dict.ContainsKey("error") ? dict["error"]?.ToString() : "Unknown server error.";
                throw new Exception(error);
            }

            // Map manually — JavaScriptSerializer won't convert snake_case to PascalCase
            return new VersionInfo
            {
                Version     = dict.ContainsKey("version")      ? dict["version"]?.ToString()      : null,
                DownloadUrl = dict.ContainsKey("download_url") ? dict["download_url"]?.ToString() : null,
                Changelog   = dict.ContainsKey("changelog")    ? dict["changelog"]?.ToString()    : null,
                ZipPassword = dict.ContainsKey("zip_password") ? dict["zip_password"]?.ToString() : null,
            };
        }

        /// <summary>
        /// Downloads a file from <paramref name="url"/> to <paramref name="destPath"/>,
        /// calling <paramref name="progressCallback"/> with percentage 0–100.
        /// Must be called from a background thread (blocks until complete).
        /// </summary>
        public void DownloadFile(string url, string destPath, Action<int> progressCallback)
        {
            using (WebClient wc = CreateWebClient())
            {
                // Sync download with progress — safe to call from BackgroundWorker
                bool done = false;
                Exception error = null;

                wc.DownloadProgressChanged += (s, e) =>
                    progressCallback?.Invoke(e.ProgressPercentage);

                wc.DownloadFileCompleted += (s, e) =>
                {
                    error = e.Error;
                    done  = true;
                };

                wc.DownloadFileAsync(new Uri(url), destPath);

                while (!done)
                    System.Threading.Thread.Sleep(50);

                if (error != null) throw error;
            }
        }

        // ------------------------------------------------------------------ //
        //  Helpers                                                             //
        // ------------------------------------------------------------------ //

        private string Post(NameValueCollection fields)
        {
            // Force TLS 1.2 (same pattern as DiscordNotifier.cs in the main project)
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            using (WebClient wc = CreateWebClient())
            {
                try
                {
                    byte[] bytes = wc.UploadValues(ApiUrl, "POST", fields);
                    return Encoding.UTF8.GetString(bytes);
                }
                catch (WebException ex) when (ex.Response != null)
                {
                    // Read the response body so we can parse the JSON error message
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new System.IO.StreamReader(stream, Encoding.UTF8))
                        return reader.ReadToEnd();
                }
            }
        }

        private static WebClient CreateWebClient()
        {
            var wc = new WebClient();
            wc.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            return wc;
        }
    }
}
