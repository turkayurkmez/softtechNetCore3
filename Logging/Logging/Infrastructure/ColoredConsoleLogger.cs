namespace Logging.Infrastructure
{
    public class ColoredConsoleLogger : ILogger
    {
        private string name;
        private ColoredConsoleLoggingConfiguration configuration;

        public ColoredConsoleLogger(string name, ColoredConsoleLoggingConfiguration configuration)
        {
            this.name = name;
            this.configuration = configuration;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == configuration.LogLevel;
        }

        private static object _lock = new object();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            lock (_lock)
            {
                if (configuration.EventId == 0 || configuration.EventId == eventId.Id)
                {
                    var existingColor = Console.ForegroundColor;
                    Console.ForegroundColor = configuration.Color;
                    Console.WriteLine($"{logLevel} - {eventId.Id} - {formatter(state, exception)}");
                    Console.ForegroundColor = existingColor;
                }
            }

        }
    }
}
