using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Logging
{
    public class LoggerFactory
    {
        private LoggerFactory()
        {

        }
        static readonly object _lock = new object();
        private static FileLogger _fileLogger;


        public static FileLogger FileLogManager()
        {
            if (_fileLogger == null)
            {
                lock (_lock)
                {
                    if (_fileLogger == null)
                        _fileLogger = new FileLogger();
                }
            }
            return _fileLogger;
        }
    }
}
