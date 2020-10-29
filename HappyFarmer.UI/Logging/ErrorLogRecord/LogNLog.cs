using NLog;

namespace HappyFarmer.UI.Logging.ErrorLogRecord
{
    public class LogNLog : ILog
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public LogNLog()
        {

        }
        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}
