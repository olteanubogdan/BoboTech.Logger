using System.Configuration;

namespace BoboTech.Logger
{
    class Settings
    {
        public static class Log
        {
            /// <summary>
            /// Trace
            /// </summary>
            public static string Level => ConfigurationManager.AppSettings[$"{nameof(Log)}.{nameof(Level)}"];

            /// <summary>
            /// Any of the System.Environment.SpecialFolder enum. Most common LocalApplicationData or CommonApplicationData.
            /// </summary>
            public static string Location => ConfigurationManager.AppSettings[$"{nameof(Log)}.{nameof(Location)}"];
        }

        public static class App
        {
            /// <summary>
            /// BoboTech
            /// </summary>
            public static string Company => ConfigurationManager.AppSettings[$"{nameof(App)}.{nameof(Company)}"];

            /// <summary>
            /// Logger
            /// </summary>
            public static string Name => ConfigurationManager.AppSettings[$"{nameof(App)}.{nameof(Name)}"];
        }
    }
}