using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Linq;

namespace BoboTech.Logger
{
    /// <summary>
    /// NLog wrapper
    /// </summary>
    public class Log
    {
        static readonly Lazy<Log> _instance = new Lazy<Log>(() => new Log());

        /// <summary>
        /// Singleton instance
        /// </summary>
        internal static Log Instance => _instance.Value;

        /// <summary>
        /// Internal NLog logger
        /// </summary>
        internal NLog.Logger InternalLogger { get; set; }

        /// <summary>
        /// Default private constructor
        /// </summary>
        Log()
        {
            LayoutRenderer.Register("errorId", (logEvent) =>
            {
                if (logEvent.Parameters == null || logEvent.Parameters.ToList().All(x => !(x is NLogInfo)))
                    return string.Empty;
                return (logEvent.Parameters.FirstOrDefault(x => x is NLogInfo) as NLogInfo).ErrorId;
            });
            LayoutRenderer.Register("caller", (logEvent) =>
            {
                if (logEvent.Parameters == null || logEvent.Parameters.ToList().All(x => !(x is NLogInfo)))
                    return string.Empty;
                return (logEvent.Parameters.FirstOrDefault(x => x is NLogInfo) as NLogInfo).Caller;
            });
            var jsonLayout = new JsonLayout { IncludeAllProperties = true };
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "time", Layout = Layout.FromString("${longdate}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "level", Layout = Layout.FromString("${level}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "message", Layout = Layout.FromString("${message}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "appdomain", Layout = Layout.FromString("${appdomain}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "processname", Layout = Layout.FromString("${processname}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "threadid", Layout = Layout.FromString("${threadid}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "caller", Layout = Layout.FromString("${caller}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "errorId", Layout = Layout.FromString("${errorId}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "exceptionType", Layout = Layout.FromString("${exception:format=Type}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "exceptionMessage", Layout = Layout.FromString("${exception:format=Message}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "exceptionStackTrace", Layout = Layout.FromString("${exception:format=StackTrace}") });
            var config = new LoggingConfiguration { DefaultCultureInfo = System.Globalization.CultureInfo.InvariantCulture };
            var jsonTarget = new FileTarget
            {
                Name = "JsonLog",
                FileName = Layout.FromString($"${{specialfolder:folder={Settings.Log.Location}}}/{Settings.App.Company}/{Settings.App.Name}/logs/${{shortdate}}.JsonLog"),
                Layout = jsonLayout
            };
            var fileTarget = new FileTarget
            {
                Name = "FileLog",
                FileName = Layout.FromString($"${{specialfolder:folder={Settings.Log.Location}}}/{Settings.App.Company}/{Settings.App.Name}/logs/${{shortdate}}.log"),
                Layout = Layout.FromString("${longdate} ${pad:padding=5:inner=${level}} [${threadid}] ${caller}: ${message}${onexception:inner=${newline}${trim-whitespace:trimWhiteSpace=true:inner=${errorId} ${exception:format=toString}}}")
            };
            var consoleTarget = new ConsoleTarget
            {
                DetectConsoleAvailable = true,
                Error = false,
                Name = "ConsoleLog",
                Layout = Layout.FromString("${longdate} ${pad:padding=5:inner=${level}} [${threadid}] ${message}")
            };
            var debuggerTarget = new DebuggerTarget
            {
                Name = "DebuggerLog",
                Layout = Layout.FromString("${longdate} ${processname} ${pad:padding=5:inner=${level}} [${threadid}] ${caller}: ${message}${onexception:inner=${newline}${errorId} ${exception:format=toString}}")
            };
            config.AddTarget(jsonTarget);
            config.AddTarget(fileTarget);
            config.AddTarget(consoleTarget);
            config.AddTarget(debuggerTarget);
            config.AddRule(LogLevel.FromString(Settings.Log.Level), LogLevel.Fatal, jsonTarget.Name);
            config.AddRule(LogLevel.FromString(Settings.Log.Level), LogLevel.Fatal, fileTarget.Name);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget.Name);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, debuggerTarget.Name);
            LogManager.ThrowExceptions = true;
            LogManager.ThrowConfigExceptions = true;
            LogManager.GlobalThreshold = LogLevel.Trace;
            LogManager.Configuration = config;
            InternalLogger = LogManager.GetLogger("*");
        }

        /// <summary>
        /// Write message to trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        public static void Trace(string message, string caller) => Instance.InternalLogger.Trace(message, new NLogInfo { Caller = caller });

        /// <summary>
        /// Write message to debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        public static void Debug(string message, string caller) => Instance.InternalLogger.Debug(message, new NLogInfo { Caller = caller });

        /// <summary>
        /// Write message to info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        public static void Info(string message, string caller) => Instance.InternalLogger.Info(message, new NLogInfo { Caller = caller });

        /// <summary>
        /// Write message to warn
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        public static void Warn(string message, string caller) => Instance.InternalLogger.Warn(message, new NLogInfo { Caller = caller });

        /// <summary>
        /// Write message to error
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        public static void Error(Exception ex, string message, string caller) => Instance.InternalLogger.Error(ex, message, new NLogInfo { Caller = caller });

        /// <summary>
        /// Write message to error
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        /// <param name="errorId"></param>
        public static void Error(Exception ex, string message, string caller, string errorId) => Instance.InternalLogger.Error(ex, message, new NLogInfo { Caller = caller, ErrorId = errorId });

        /// <summary>
        /// Write message to error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        public static void Error(string message, string caller) => Instance.InternalLogger.Error(message, new NLogInfo { Caller = caller });
    }
}