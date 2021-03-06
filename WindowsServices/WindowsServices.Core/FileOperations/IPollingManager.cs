﻿using System;
using System.Threading.Tasks;

namespace WindowsServices.Core.FileOperations
{
    public interface IPollingManager
    {
        int WaitInterval { get; }

        int AccessCount { get; }

        Task Perform<TException>(Func<Task> action) where TException : Exception;
    }
}
