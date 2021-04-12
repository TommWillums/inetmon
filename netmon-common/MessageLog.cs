using System;

namespace NetMon.Common
{
    public class MessageLog
    {
        public const string Info = "Info";
        public const string Ok = "Ok";
        public const string Error = "Error";

        public int Id { get; set; }
        public DateTime LogDate { get; init; }
        public string Type { get; init; }
        public string Message { get; init; }
    }
}
