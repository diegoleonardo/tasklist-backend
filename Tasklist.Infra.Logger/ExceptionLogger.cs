using System;
using System.Collections.Generic;

namespace Tasklist.Infra.Logger
{
    public class ExceptionLogger : ILog
    {
        private readonly IList<string> _logs;
        public ExceptionLogger()
        {
            _logs = new List<string>();
        }
        public void Log(string message)
        {
            _logs.Add(message);
        }
    }
}
