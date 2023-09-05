namespace Simulation.Server
{
    internal static class Logger
    {
        public static event EventHandler<LogEvent>? OnMessage;

        public static void Log(object? caller, string message, ConsoleColor? color = null)
        {
            OnMessage?.Invoke(caller, new LogEvent
            {
                Color = color,
                Message = message
            });
        }
    }
}