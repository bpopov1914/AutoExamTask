﻿using NLog;

namespace AutoExamTask.Utilities
{
    public static class Logger
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();
    }
}
