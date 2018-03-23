using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Repositories.Logging
{
    public interface ILoggingRepository
    {      
        string LogException<T>(Exception e);
        void LogDebug<T>(string msg);
        void LogWarning<T>(string msg);
        void LogVerbose<T>(string msg);
    }
}
