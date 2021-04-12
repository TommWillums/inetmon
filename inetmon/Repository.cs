using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NetMon.Common;

namespace inetmon
{
    enum LogType { Header, Started, Alive, Ok, Error }

    public class Repository : IRepository
    {
        List<MessageLog> _log;

        public Repository() { }

        public int Fill(List<MessageLog> data)
        {
            if (data == null)
                _log = new List<MessageLog>();
            else
                _log = data;
            return _log.Count;
        }

        public List<MessageLog> GetLog(DateTime fromDate)
        {
            return _log.FindAll(l => l.LogDate >= fromDate);
        }

        public List<MessageLog> GetInfoLog(DateTime fromDate)
        {
            return _log.FindAll(l => l.LogDate >= fromDate && l.Type == MessageLog.Info);
        }

        public List<MessageLog> GetErrorLog(DateTime fromDate)
        {
            return _log.FindAll(l => l.LogDate >= fromDate && l.Type == MessageLog.Error);
        }
    }
}
