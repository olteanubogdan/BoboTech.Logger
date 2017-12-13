using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoboTech.Logger.UnitTest
{
    /// <summary>
    /// Logger tests class.
    /// </summary>
    [TestClass]
    public class LoggerTest
    {
        /// <summary>
        /// Tests the default settings
        /// </summary>
        [TestMethod]
        public void TestDefaultSettings()
        {
            Assert.AreEqual(Settings.App.Company, "BoboTech");
            Assert.AreEqual(Settings.App.Name, "Logger");
            Assert.AreEqual(Settings.Log.Level, Debugger.IsAttached ? "Trace" : "Debug");
            Assert.AreEqual(Settings.Log.Location, "CommonApplicationData");
        }

        /// <summary>
        /// Tests logging capabilities
        /// </summary>
        [TestMethod]
        public void TestLogger()
        {
            var caller = $"{nameof(LoggerTest)}.{nameof(TestLogger)}";
            Log.Trace("Trace message", caller); //this will do nothing because the test runner does not have a debugger attached
            Log.Debug("Debug message", caller);
            Log.Info("Info message", caller);
            Log.Warn("Warn message", caller);
            Log.Error("Error message", caller);
            Log.Error(new Exception("First exception test"), "Exception message.", caller);
            Log.Error(new Exception("Second exception test"), "Exception message with error id.", caller, $"{DateTime.Now:yyyyMMdd_hhmmss}");
            Log.Error(new Exception("Third exception test"), $"Exception message{Environment.NewLine}with new line.", caller);
        }
    }
}