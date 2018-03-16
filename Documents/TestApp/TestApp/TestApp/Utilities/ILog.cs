namespace Aglive.Business.Infrastructure.Utilities
{
    using Ninject;
    using System;

    public interface ILog : IInitializable
    {
        bool CanLogInfo { get; set; }
        bool CanLogError { get; set; }

        void Debug(string message, Exception ex = null);

        void Debug(string message, params object[] ps);

        void Error(string message, Exception ex = null);

        void Error(string message, params object[] ps);

        void Fatal(string message, Exception ex = null);

        void Fatal(string message, params object[] ps);

        void Info(string message, Exception ex = null);

        void Info(string message, params object[] ps);

        void Warn(string message, Exception ex = null);

        void Warn(string message, params object[] ps);

        void Trace(string message, Exception ex = null);

        void Trace(string message, params object[] ps);
    }
}