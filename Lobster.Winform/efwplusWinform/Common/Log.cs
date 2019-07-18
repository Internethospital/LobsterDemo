using log4net;
using System;
namespace efwplusWinform.Common
{
	public class Log
	{
        public static LogHandler handler;
        private static readonly ILog log = LogManager.GetLogger("File");
        public static void Debug(object message)
        {
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Debug(message);
        }
		public static void Debug(object message, Exception exp)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Debug(message, exp);
		}
		public static void DebugFormat(string format, object arg0)
		{
            Log.log.DebugFormat(format, arg0);
		}
		public static void DebugFormat(string format, params object[] args)
		{
			Log.log.DebugFormat(format, args);
		}
		public static void DebugFormat(IFormatProvider provider, string format, params object[] args)
		{
			Log.log.DebugFormat(provider, format, args);
		}
		public static void DebugFormat(string format, object arg0, object arg1)
		{
			Log.log.DebugFormat(format, arg0, arg1);
		}
		public static void DebugFormat(string format, object arg0, object arg1, object arg2)
		{
			Log.log.DebugFormat(format, arg0, arg1, arg2);
		}
		public static void Error(object message)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Error(message);
		}
		public static void Error(object message, Exception exception)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Error(message, exception);
		}
		public static void ErrorFormat(string format, object arg0)
		{
			Log.log.ErrorFormat(format, arg0);
		}
		public static void ErrorFormat(string format, params object[] args)
		{
			Log.log.ErrorFormat(format, args);
		}
		public static void ErrorFormat(IFormatProvider provider, string format, params object[] args)
		{
			Log.log.ErrorFormat(provider, format, args);
		}
		public static void ErrorFormat(string format, object arg0, object arg1)
		{
			Log.log.ErrorFormat(format, arg0, arg1);
		}
		public static void ErrorFormat(string format, object arg0, object arg1, object arg2)
		{
			Log.log.ErrorFormat(format, arg0, arg1, arg2);
		}
		public static void Fatal(object message)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Fatal(message);
		}
		public static void Fatal(object message, Exception exception)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Fatal(message, exception);
		}
		public static void FatalFormat(string format, object arg0)
		{
			Log.log.FatalFormat(format, arg0);
		}
		public static void FatalFormat(string format, params object[] args)
		{
			Log.log.FatalFormat(format, args);
		}
		public static void FatalFormat(IFormatProvider provider, string format, params object[] args)
		{
			Log.log.FatalFormat(provider, format, args);
		}
		public static void FatalFormat(string format, object arg0, object arg1)
		{
			Log.log.FatalFormat(format, arg0, arg1);
		}
		public static void FatalFormat(string format, object arg0, object arg1, object arg2)
		{
			Log.log.FatalFormat(format, arg0, arg1, arg2);
		}
		public static void Info(object message)
		{
			Log.log.Info(message);
		}
		public static void Info(object message, Exception exception)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Info(message, exception);
		}
		public static void InfoFormat(string format, object arg0)
		{
			Log.log.InfoFormat(format, arg0);
		}
		public static void InfoFormat(string format, params object[] args)
		{
			Log.log.InfoFormat(format, args);
		}
		public static void InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			Log.log.InfoFormat(provider, format, args);
		}
		public static void InfoFormat(string format, object arg0, object arg1)
		{
			Log.log.InfoFormat(format, arg0, arg1);
		}
		public static void InfoFormat(string format, object arg0, object arg1, object arg2)
		{
			Log.log.InfoFormat(format, arg0, arg1, arg2);
		}
		public static void Warn(object message)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Warn(message);
		}
		public static void Warn(object message, Exception exception)
		{
            if (handler != null)
            {
                handler(DateTime.Now, message);
            }
            Log.log.Warn(message, exception);
		}
		public static void WarnFormat(string format, object arg0)
		{
			Log.log.WarnFormat(format, arg0);
		}
		public static void WarnFormat(string format, params object[] args)
		{
			Log.log.WarnFormat(format, args);
		}
		public static void WarnFormat(IFormatProvider provider, string format, params object[] args)
		{
			Log.log.WarnFormat(provider, format, args);
		}
		public static void WarnFormat(string format, object arg0, object arg1)
		{
			Log.log.WarnFormat(format, arg0, arg1);
		}
		public static void WarnFormat(string format, object arg0, object arg1, object arg2)
		{
			Log.log.WarnFormat(format, arg0, arg1, arg2);
		}
	}

    public delegate void LogHandler(DateTime time, object msg);
}
