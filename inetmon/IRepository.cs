using System;
using System.Collections.Generic;
using NetMon.Common;

namespace inetmon
{
    public interface IRepository
    {
        int Fill(List<MessageLog> data);
        List<MessageLog> GetErrorLog(DateTime fromDate);
        List<MessageLog> GetInfoLog(DateTime fromDate);
        List<MessageLog> GetLog(DateTime fromDate);
    }
}