using System.Collections.Concurrent;

namespace Logging.Infrastructure
{
    public class ColoredConsoleLoggerProvider : ILoggerProvider
    {
        private ColoredConsoleLoggingConfiguration configuration;
        private ConcurrentDictionary<string, ColoredConsoleLogger> loggers = new ConcurrentDictionary<string, ColoredConsoleLogger>();

        public ColoredConsoleLoggerProvider(ColoredConsoleLoggingConfiguration configuration)
        {
            this.configuration = configuration;
            //this.configuration.Color = ConsoleColor.Yellow;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new ColoredConsoleLogger(name, configuration));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
