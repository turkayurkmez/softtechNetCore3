namespace Logging.Infrastructure
{
    public class ColoredConsoleLoggingConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
        public ConsoleColor Color { get; set; } = ConsoleColor.Red;
    }
}
