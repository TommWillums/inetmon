using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace inetmon
{
    enum LogType { Header, Started, Alive, Ok, Error }

    public class Repository
    {
        List<MessageLog> _log;

        public Repository(List<MessageLog> log = null)
        {
            if (log == null)
                _log = new List<MessageLog>();
            else
                _log = log;
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

        public int BuildMessagelogFromFile(string filename)
        {
            using StreamReader file = new(filename);
            //var loglist = new List<MessageLog>();
            string line = "";
            MessageLog logitem;
            int id = 0;
            while (true)
            {
                line = file.ReadLine();
                if (line == null)
                    break;
                logitem = ParseLogLine(line);
                logitem.Id = ++id;
                _log.Add(logitem);
            }
            return id;
        }

        private MessageLog ParseLogLine(string line)
        {
            DateTime dateTime = DateTime.Now;
            string type;
            string message;

            if (line.Contains("ERROR:"  ))
            {
                // 22.03.2021 15:25:02 ERROR: Ping www.google.com failed!
                string str = line[..19];
                dateTime = DateTime.ParseExact(str, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                type = MessageLog.Error;
                message = line[20..];
            }
            else if (line.Contains("- OK"))
            {
                // 22.03.2021 15:25 - OK
                string str = line[..16];
                dateTime = DateTime.ParseExact(str, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                type = MessageLog.Ok;
                message = line[19..];
            }
            else
            {
                type = MessageLog.Info;
                message = line;
            }

            return new MessageLog { Type = type, LogDate = dateTime, Message = message };
        }
    }
}
