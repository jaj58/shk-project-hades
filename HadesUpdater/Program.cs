using System;
using System.Windows.Forms;

namespace HadesUpdater
{
    internal static class Program
    {
        /// <summary>
        /// Path of the exe to relaunch after a successful update.
        /// Set from the --relaunch "path" command-line argument.
        /// </summary>
        public static string RelaunchPath { get; private set; }

        [STAThread]
        static void Main(string[] args)
        {
            // Parse command-line arguments
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "--relaunch", StringComparison.OrdinalIgnoreCase)
                    && i + 1 < args.Length)
                {
                    RelaunchPath = args[i + 1];
                    i++; // consume next arg
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Returns true if <paramref name="serverVersion"/> is newer than <paramref name="localVersion"/>.
        /// Handles standard x.y.z semver and dev builds (dev-&lt;sha&gt;) — any SHA change triggers an update.
        /// </summary>
        public static bool IsNewerVersion(string serverVersion, string localVersion)
        {
            // Dev builds use a non-semver label — any change in the string means a new build
            if (serverVersion.StartsWith("dev-", StringComparison.OrdinalIgnoreCase) ||
                localVersion.StartsWith("dev-", StringComparison.OrdinalIgnoreCase))
            {
                return !string.Equals(serverVersion, localVersion, StringComparison.OrdinalIgnoreCase);
            }

            try
            {
                Version server = new Version(serverVersion);
                Version local  = new Version(localVersion);
                return server > local;
            }
            catch { return false; }
        }
    }
}
