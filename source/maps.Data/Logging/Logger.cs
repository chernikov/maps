using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Logging
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger _logger = LogManager.GetLogger("DB Logger");

        public void Information(string message)
        {
            _logger.Trace(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            _logger.Trace(string.Format(fmt, vars));
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            _logger.Trace(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            _logger.Warn(string.Format(fmt, vars));
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            _logger.Warn(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            _logger.Error(string.Format(fmt, vars));
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            _logger.Error(FormatExceptionMessage(exception, fmt, vars));
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";Properties:", properties);
            _logger.Info(message);
        }

        private static string FormatExceptionMessage(Exception exception, string fmt, object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(exception.ToString());
            return sb.ToString();
        }
    }
}
