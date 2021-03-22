using System;
using System.Collections.Generic;

namespace inetmon
{
    public class Repository
    {
        List<ErrorLog> _errorLog;

        public Repository(List<ErrorLog> errorLog = null)
        {
            if (errorLog == null)
                _errorLog = new List<ErrorLog>();
            else
                _errorLog = errorLog;
        }

        public List<ErrorLog> GetErrorLog()
        {
            return _errorLog;
        }

        public List<ErrorLog> GetErrorLog(DateTime fromDate)
        {
            return _errorLog.FindAll(q => q.ErrorTime >= fromDate);
        }

        public List<ErrorLog> BuildErrorlogFromFile(string filename)
        {
            return null;
        }
    }
}
