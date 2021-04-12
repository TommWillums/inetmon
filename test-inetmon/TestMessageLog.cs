using System;
using System.Collections.Generic;
using Xunit;
using inetmon;
using NetMon.Common;
using inetmon.DAL;

namespace test_inetmon
{
    public class TestMessageLog
    {       
        private List<MessageLog> InitLogList()
        {
            List<MessageLog> messageLog = new List<MessageLog>();
            messageLog.Add(new MessageLog { Id = 1, LogDate = DateTime.Now.AddDays(-10), Type = "Info", Message = "---- Initialized" });
            messageLog.Add(new MessageLog { Id = 2, LogDate = DateTime.Now.AddMonths(-2), Type = "Started", Message = "Log initiated" });
            messageLog.Add(new MessageLog { Id = 3, LogDate = DateTime.Now.AddDays(-5), Type = "Alive", Message = "Yes, still here" });
            messageLog.Add(new MessageLog { Id = 4, LogDate = DateTime.Now.AddDays(-9), Type = "Error", Message = "This is the first error" });
            messageLog.Add(new MessageLog { Id = 5, LogDate = DateTime.Now.AddMonths(-2), Type = "Error", Message = "This error happened two months ago" });
            messageLog.Add(new MessageLog { Id = 6, LogDate = DateTime.Now.AddDays(-1), Type = "Error", Message = "This happened yesterday" });
            messageLog.Add(new MessageLog { Id = 7, LogDate = DateTime.Now.AddDays(-3), Type = "Ok", Message = "Reported 3 days ago" });
            return messageLog;
        }

        [Fact]
        public void TestLogAndRepository()
        {
            var list = InitLogList();
            var repository = new Repository();
            repository.Fill(list);
            var result = repository.GetLog(DateTime.MinValue);
            Assert.Equal(7, list.Count);
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public void TestErrorLogFromLastMonth()
        {
            var list = InitLogList();
            var repository = new Repository();
            repository.Fill(list);
            var result = repository.GetErrorLog(DateTime.Now.AddMonths(-1));
            Assert.Equal(2, result.Count);
        }

        // Data from Testlog.log
        const string LOGFILE = "../../../../Testlog.log";
        [Fact]
        public void TestReadingLogfile()
        {
            var list = new MessageLogHelper().GetMessagelogFromFile(LOGFILE);
            var repository = new Repository();
            repository.Fill(list);
            var result = repository.GetLog(DateTime.MinValue);
            Assert.Equal(18, result.Count);
        }

        [Fact]
        public void TestReadingInfologFromFile()
        {
            var list = new MessageLogHelper().GetMessagelogFromFile(LOGFILE);
            var repository = new Repository();
            repository.Fill(list);
            var result = repository.GetInfoLog(DateTime.MinValue);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void TestReadingErrorlogFromFile()
        {
            var list = new MessageLogHelper().GetMessagelogFromFile(LOGFILE);
            var repository = new Repository();
            repository.Fill(list);
            var result = repository.GetErrorLog(DateTime.MinValue);
            Assert.Equal(4, result.Count);
        }
    }
}
