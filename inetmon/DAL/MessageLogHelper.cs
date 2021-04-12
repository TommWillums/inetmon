using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NetMon.Common;

namespace inetmon.DAL
{
    public class MessageLogHelper
    {
        public List<MessageLog> GetMessageLogFromDatabase()
        {
            var loglist = new List<MessageLog>();
            return loglist;
        }

        public List<MessageLog> GetMessagelogFromFile(string filename)
        {
            using StreamReader file = new(filename);
            var loglist = new List<MessageLog>();
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
                loglist.Add(logitem);
            }
            return loglist;
        }

        private MessageLog ParseLogLine(string line)
        {
            DateTime dateTime = DateTime.Now;
            string type;
            string message;

            if (line.Contains("ERROR:"))
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
