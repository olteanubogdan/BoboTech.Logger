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
        /// <summary>
        /// Log settings class.
        /// </summary>
        public static class Log
        {
            static Lazy<string> _level = new Lazy<string>(() => ConfigurationManager.AppSettings.AllKeys.Contains($"{nameof(Log)}.{nameof(Level)}") ? ConfigurationManager.AppSettings[$"{nameof(Log)}.{nameof(Level)}"] : (Debugger.IsAttached ? LogLevel.Trace.Name : LogLevel.Debug.Name));

            /// <summary>
            /// Level setting. Defaults to Trace if in debug, Debug if otherwise.
            /// </summary>
            public static string Level => _level.Value;

            static Lazy<string> _location = new Lazy<string>(() => ConfigurationManager.AppSettings.AllKeys.Contains($"{nameof(Log)}.{nameof(Location)}") ? ConfigurationManager.AppSettings[$"{nameof(Log)}.{nameof(Location)}"] : Environment.SpecialFolder.CommonApplicationData.ToString());

            /// <summary>
            /// Location settings. Can be any of the System.Environment.SpecialFolder enum.
            /// Most common LocalApplicationData or CommonApplicationData. Defaults to CommonApplicationData.
            /// </summary>
            public static string Location => _location.Value;
        }

        /// <summary>
        /// App settins class
        /// </summary>
        public static class App
        {
            static Lazy<string> _company = new Lazy<string>(() => ConfigurationManager.AppSettings.AllKeys.Contains($"{nameof(App)}.{nameof(Company)}") ? ConfigurationManager.AppSettings[$"{nameof(App)}.{nameof(Company)}"] : "BoboTech");

            /// <summary>
            /// Company settings. Defaults to BoboTech.
            /// </summary>
            public static string Company => _company.Value;

            static Lazy<string> _name = new Lazy<string>(() => ConfigurationManager.AppSettings.AllKeys.Contains($"{nameof(App)}.{nameof(Company)}") ? ConfigurationManager.AppSettings[$"{nameof(App)}.{nameof(Company)}"] : "Logger");

            /// <summary>
            /// App name setting. Defaults to Logger.
            /// </summary>
            public static string Name => _name.Value;
        }
    }
}