using Microsoft.Extensions.Logging;

namespace TesteArquitetura.Core.Log
{
    public class Elmah : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(string? message)
        {
            this.LogInformation(null, message);
        }
        public void LogInformation(Exception? exception, string? message)
        {
            this.LogInformation(0, exception, message);
        }


        public void LogWarning(string? message)
        {
            this.LogWarning(null, message);
        }
        public void LogWarning(Exception? exception, string? message)
        {
            this.LogWarning(0, exception, message);
        }


        public void LogError(string? message)
        {
            this.LogError(null, message);
        }
        public void LogError(Exception? exception, string? message)
        {
            this.LogError(0, exception, message);
        }

        public void LogCritical(string? message)
        {
            this.LogCritical(null, message);
        }
        public void LogCritical(Exception? exception, string? message)
        {
            this.LogCritical(0, exception, message);
        }

    }
}
