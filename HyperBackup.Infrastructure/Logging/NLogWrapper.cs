using System;
using HyperBackup.Core.Interfaces;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace HyperBackup.Infrastructure.Logging
{
    public class NLogWrapper : ILog
    {
        private readonly IApplicationConfig _config;
        private readonly Logger _logger = LogManager.GetLogger("NLog");

        public NLogWrapper(IApplicationConfig applicationConfig)
        {
            _config = applicationConfig;
            ConfigureLogger();
        }

        public void Trace(string message)
        {
            Log(LogLevel.Trace, message);
        }

        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void Warn(string message)
        {
            Log(LogLevel.Warn, message);
        }

        public void Error(string message, Exception exception = null)
        {
            Log(LogLevel.Error, message);
            if (exception != null)
                Trace(exception.ToString());
        }

        public void Fatal(string message, Exception exception = null)
        {
            Log(LogLevel.Fatal, message);
            if (exception != null)
                Trace(exception.ToString());
        }

        private void ConfigureLogger()
        {
            var loggingConfig = new LoggingConfiguration();
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("LogLevelIndicator", typeof(LogLevelIndicatorLayoutRenderer));

            // Configure File as Logging Target
            var fileTarget = new FileTarget
            {
                FileName = "${basedir}/HyperBackup.log",
                Layout = "${longdate} ${LogLevelIndicator} ${message}"
            };
            loggingConfig.AddTarget("file", fileTarget);
            loggingConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.FromString(_config.FileLogLevel), fileTarget));

            // Configure Console as Logging Target
            if (_config.LogToConsole)
            {
                var consoleTarget = new ColoredConsoleTarget
                {
                    Layout = @"[${level:uppercase=true}] ${message}"
                };
                loggingConfig.AddTarget("console", consoleTarget);
                loggingConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.FromString(_config.ConsoleLogLevel), consoleTarget));
            }

            LogManager.Configuration = loggingConfig;
        }

        private void Log(LogLevel logLevel, string message)
        {
            _logger.Log(logLevel, message);
        }
    }
}
