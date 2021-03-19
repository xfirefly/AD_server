using System;
using System.Runtime.InteropServices;

namespace Common
{
    internal class Log
    {
        private NLog.Logger _logger;

        private Log(NLog.Logger logger)
        {
            this._logger = logger;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();

        static Log()
        {
            //Default = new Log(NLog.LogManager.GetCurrentClassLogger());
#if DEBUG
            AllocConsole();
#endif
        }

        public static void Free()
        {
#if DEBUG
            FreeConsole();
#endif
        }

        public Log(string name)
                : this(NLog.LogManager.GetLogger(name))
        {
        }

        public Log(Type this_GetType)
                : this(NLog.LogManager.GetLogger(this_GetType.Name))
        {
        }

        //public static Log Default { get; private set; }

        public void t(string msg, params object[] args)
        {
            _logger.Trace(msg, args);
        }

        public void t(string msg, Exception err)
        {
            _logger.Trace(err, msg);
        }

        public void d(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        public void d(string msg, Exception err)
        {
            _logger.Debug(err, msg);
        }

        public void i(string msg, params object[] args)
        {
            _logger.Info(msg, args);
        }

        public void i(string msg, Exception err)
        {
            _logger.Info(err, msg);
        }

        public void w(string msg, params object[] args)
        {
            _logger.Warn(msg, args);
        }

        public void w(string msg, Exception err)
        {
            _logger.Warn(err, msg);
        }

        public void e(string msg, params object[] args)
        {
            _logger.Error(msg, args);
        }

        public void e(string msg, Exception err)
        {
            _logger.Error(err, msg);
        }

        public void f(string msg, params object[] args)
        {
            _logger.Fatal(msg, args);
        }

        public void f(string msg, Exception err)
        {
            _logger.Fatal(err, msg);
        }
    }
}