using Microsoft.Win32;
using System;
using System.Text;

namespace DWR_API.Helpers
{
    public static class Utilities
    {
        public static class RegEdit
        {
            private const string Timer_key = "TIMER_MINUTE";
            private const string SPath_key = "SERVICE_PATH";
            public static string SystemName { get; set; }
            public static string GetRegValue(string key)
            {
                if (string.IsNullOrEmpty(SystemName))
                {
                    throw new NullReferenceException("Utilities SystemName is NULL please fill it!!!");
                }

                try
                {
                    using (RegistryKey Regkey = Registry.CurrentConfig.OpenSubKey($@"SOFTWARE\{SystemName}"))
                    {
                        if (Regkey != null)
                        {
                            return (string)Regkey.GetValue(key, string.Empty);
                        }
                    }

                    return string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            public static void SetRegValue(string key, string value)
            {
                if (string.IsNullOrEmpty(SystemName))
                {
                    throw new NullReferenceException("Utilities SystemName is NULL please fill it!!!");
                }
                try
                {
                    using (RegistryKey Regkey = Registry.CurrentConfig.OpenSubKey($@"SOFTWARE\{SystemName}"))
                    {
                        if (Regkey != null)
                        {
                            Regkey.SetValue(key, value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static string getTimerMinute(string extendKey = null)
            {
                string key = Timer_key;

                if (!string.IsNullOrEmpty(extendKey))
                {
                    key = $"{key}{extendKey}";
                }

                return GetRegValue(key);
            }

            public static string getSPath(string extendKey = null)
            {
                string key = SPath_key;

                if (!string.IsNullOrEmpty(extendKey))
                {
                    key = $"{key}{extendKey}";
                }

                return GetRegValue(key);
            }
        }
    }
}
