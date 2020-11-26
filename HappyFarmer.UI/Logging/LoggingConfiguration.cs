using Serilog.Core;

namespace HappyFarmer.UI.Logging
{
    public abstract class LoggingConfiguration
    {
        protected abstract Logger GetLogger();

        public Logger InstanceLogger()
        {
            return default(Logger);
        }
    }
}
