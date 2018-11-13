using NLog;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace BoboTech.Logger
{
    /// <summary>
    /// Settings class.
    /// </summary>
    public class Settings
    {
        static Lazy<string> GetLazyValue(string keyName, string defaultValue) => new Lazy<string>(() => ConfigurationManager.AppSettings.AllKeys.Contains(keyName) ? ConfigurationManager.AppSettings[keyName] : defaultValue);

        delegate bool TryParseHandler<T>(string value, out T result);

        static Lazy<T> GetLazyValue<T>(string keyName, T defaultValue, TryParseHandler<T> tryParse) => new Lazy<T>(() =>
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(keyName))
                return defaultValue;
            if (tryParse(ConfigurationManager.AppSettings[keyName], out T value))
                return value;
            return defaultValue;
        });

        /// <summary>
        /// Log settings class.
        /// </summary>
        public static class Log
        {
            static Lazy<string> _level = GetLazyValue($"{nameof(Log)}.{nameof(Level)}", Debugger.IsAttached ? LogLevel.Trace.Name : LogLevel.Debug.Name);

            /// <summary>
            /// Level setting. Defaults to Trace if Debugger.IsAttached, Debug otherwise. Available values are (in order): Trace, Debug, Info, Warn, Error, Fatal.
            /// </summary>
            public static string Level => _level.Value;

            static Lazy<string> _location = GetLazyValue($"{nameof(Log)}.{nameof(Location)}", Environment.SpecialFolder.CommonApplicationData.ToString());

            /// <summary>
            /// Location settings. Can be any of the System.Environment.SpecialFolder enum.
            /// Most common are LocalApplicationData or CommonApplicationData. Defaults to CommonApplicationData.
            /// </summary>
            public static string Location => _location.Value;
        }

        /// <summary>
        /// App settins class
        /// </summary>
        public static class App
        {
            static Lazy<string> _company = GetLazyValue($"{nameof(App)}.{nameof(Company)}", "BoboTech");

            /// <summary>
            /// Company settings. Defaults to BoboTech.
            /// </summary>
            public static string Company => _company.Value;

            static Lazy<string> _name = GetLazyValue($"{nameof(App)}.{nameof(Name)}", "Logger");

            /// <summary>
            /// App name setting. Defaults to Logger.
            /// </summary>
            public static string Name => _name.Value;
        }
    }
}