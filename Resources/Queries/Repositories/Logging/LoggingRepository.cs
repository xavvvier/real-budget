using System;
using Relativity.API;
using Resources.Constants;

namespace Resources.Repositories.Logging
{
    public class LoggingRepository : ILoggingRepository
    {
        public const string DEFAULT_ERROR_MSG = "An error has occurred in the operation. Please contact your system administrator.";
        IHelper _helper;
        int _workspaceID;

        public LoggingRepository(IHelper helper, int workspaceID)
        {
            _helper = helper;
            _workspaceID = workspaceID;
        }

        private LoggingRepository()
        {

        }

        ILogFactory __loggingFactory;
        ILogFactory LoggingFactory
        {
            get
            {
                if (__loggingFactory == null)
                {
                    __loggingFactory = _helper.GetLoggerFactory();
                }
                return __loggingFactory;
            }
        }
        public LoggingRepository(ILogFactory factory)
        {
            __loggingFactory = factory;
        }

        public string LogException<T>(Exception e)
        {
            try
            {
                var apiLog = getAPILog<T>();
                apiLog.LogError(e, $"{GlobalConstants.APPNAME}, Something Unexpected has ocurred");
            }
            catch
            {

            }
            return DEFAULT_ERROR_MSG;
        }

        public void LogDebug<T>(string msg)
        {
            var apiLog = getAPILog<T>();
            apiLog.LogDebug($"{GlobalConstants.APPNAME}, tracing for Debug: {msg}", msg);
        }

        public void LogWarning<T>(string msg)
        {
            var apiLog = getAPILog<T>();
            apiLog.LogWarning($"{GlobalConstants.APPNAME}, be aware with this: {msg}", msg);
        }

        public void LogVerbose<T>(string msg)
        {
            var apiLog = getAPILog<T>();
            apiLog.LogVerbose($"{GlobalConstants.APPNAME}, tracing: {msg}", msg);
        }

        private IAPILog getAPILog<T>()
        {
            var apiLog = LoggingFactory.GetLogger().ForContext<T>();
            return apiLog;
        }
    }
}
