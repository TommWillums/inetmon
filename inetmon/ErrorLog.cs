using System;

namespace inetmon
{
    public class ErrorLog
    {
        public int Id { get; init; }
        public DateTime ErrorTime { get; init; }
        public string ErrorMessage { get; init; }
    }
}
