﻿using System;

namespace HyperBackup.Core.Interfaces
{
    public interface ILog
    {
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message, Exception exception = null);
        void Fatal(string message, Exception exception = null);
    }
}