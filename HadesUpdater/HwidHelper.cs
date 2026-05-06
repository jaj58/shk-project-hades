using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace HadesUpdater
{
    /// <summary>
    /// Generates a stable hardware fingerprint from two system values:
    /// the Windows MachineGuid (stable per Windows install) and the
    /// motherboard serial number (stable across Windows reinstalls).
    /// The result is a 32-character uppercase hex string (SHA-256 truncated).
    /// </summary>
    public static class HwidHelper
    {
        public static string GetHwid()
        {
            string machineGuid  = GetRegistryValue(@"SOFTWARE\Microsoft\Cryptography", "MachineGuid");
            string boardSerial  = GetWmiValue("Win32_BaseBoard", "SerialNumber");

            string raw = (machineGuid + "|" + boardSerial).ToLowerInvariant();

            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                return BitConverter.ToString(hash).Replace("-", "").Substring(0, 32).ToUpper();
            }
        }

        private static string GetRegistryValue(string subkey, string name)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subkey))
                    return key?.GetValue(name)?.ToString() ?? "unknown";
            }
            catch { return "unknown"; }
        }

        private static string GetWmiValue(string wmiClass, string property)
        {
            try
            {
                using (ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT " + property + " FROM " + wmiClass))
                {
                    foreach (ManagementObject obj in searcher.Get())
                        return obj[property]?.ToString()?.Trim() ?? "unknown";
                }
            }
            catch { }
            return "unknown";
        }
    }
}
