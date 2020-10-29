using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
