using System;
using System.Collections.Generic;
using Xunit;
using inetmon;

namespace test_inetmon
{
    public class TestErrorLog
    {       
        private List<ErrorLog> InitErrorLogList()
        {
            List<ErrorLog> errorLog = new List<ErrorLog>();
            errorLog.Add(new ErrorLog { Id = 1, ErrorTime = DateTime.Now, ErrorMessage = "This is the first error" });
            errorLog.Add(new ErrorLog { Id = 3, ErrorTime = DateTime.Now.AddMonths(-2), ErrorMessage = "This error happened two months ago" });
            errorLog.Add(new ErrorLog { Id = 2, ErrorTime = DateTime.Now.AddDays(-1), ErrorMessage = "This happened yesterday" });
            return errorLog;
        }

        [Fact]
        public void TestErrorLogAndRepository()
        {
            var list = InitErrorLogList();
            var repository = new Repository(list);
            var result = repository.GetErrorLog();
            Assert.Equal(3, list.Count);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void TestErrorLogFromLastMonth()
        {
            var list = InitErrorLogList();
            var repository = new Repository(list);
            var result = repository.GetErrorLog(DateTime.Now.AddMonths(-1));
            Assert.Equal(3, list.Count);
            Assert.Equal(2, result.Count);
        }
    }
}
