using NLog.Config;
using NLog.Targets;
using NLog;

namespace MurliAnveshan.Classes
{
    internal static class LoggingSetup
    {
        public static void ConfigureNLog()
        {
            // Create a new logging configuration
            var config = new LoggingConfiguration();

            // Create a file target
            var fileTarget = new FileTarget("logfile")
            {
                FileName = "logs/MurliAnveshanLogs-${shortdate}.log", // Log file path
                Layout = "${longdate} | ${level:uppercase=true} | ${logger} | ${message} ${exception:format=toString}", // Log format
                ArchiveFileName = "logs/archive/app-log-{#}.log", // Archive pattern
                ArchiveEvery = FileArchivePeriod.Friday, // Archive Weekly
                MaxArchiveFiles = 5, // Retain up to 5 archive files
                Encoding = System.Text.Encoding.UTF8 // Encoding type
            };

            // Add the target to the configuration
            config.AddTarget(fileTarget);

            // Create a rule to log all levels to the file target
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);

            // Apply the configuration to NLog
            LogManager.Configuration = config;

            // Optional: Test the configuration by logging a sample message
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("NLog has been configured programmatically.");
        }
    }
}
