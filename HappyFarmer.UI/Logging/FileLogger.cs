using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Logging
{
    public class FileLogger : LoggingConfiguration, ILogManager<FileLogger>
    {
        private const string LogFolder = @"\LogFiles\";

        public void Debug(string message)
        {
            GetLogger().Debug(message);
        }

        public void Debug(string message, FileLogger t)
        {
            GetLogger().Debug(message, t);
        }

        public void Error(string message)
        {
            GetLogger().Error(message);
        }

        public void Error(string message, FileLogger t)
        {
            GetLogger().Error(message, t);
        }

        public void Fatal(string message)
        {
            GetLogger().Fatal(message);
        }

        public void Fatal(string message, FileLogger t)
        {
            GetLogger().Fatal(message, t);
        }

        public void Information(string message)
        {
            GetLogger().Information(message);
        }

        public void Information(string message, FileLogger t)
        {
            GetLogger().Information(message, t);
        }

        public void Verbose(string message)
        {
            GetLogger().Verbose(message);
        }

        public void Verbose(string message, FileLogger t)
        {
            GetLogger().Verbose(message, t);
        }

        public void Warning(string message)
        {
            GetLogger().Warning(message);
        }

        public void Warning(string message, FileLogger t)
        {
            GetLogger().Warning(message, t);
        }
        protected override Logger GetLogger()
        {
            return new LoggerConfiguration()
                   .WriteTo.File(string.Format("{0}{1}", Directory.GetCurrentDirectory() + LogFolder, ".txt"),
                   rollingInterval: RollingInterval.Day,
                   retainedFileCountLimit: null,
                   fileSizeLimitBytes: 5000000,
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                   .CreateLogger();
        }
    }
}
