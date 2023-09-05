namespace Simulation.Server.Cli
{
    public static class Program
    {
        public static void Main()
        {
            var manager = new ServerManager();
            manager.SetOutput(OutputEventHandler);
            manager.StartServer();
            Console.Read();
        }

        private static void OutputEventHandler(object? sender, LogEvent e)
        {
            var bak = Console.ForegroundColor;
            if (e.Color.HasValue)
            {
                Console.ForegroundColor = e.Color.Value;
            }
            Console.WriteLine(e.Message);
            Console.ForegroundColor = bak;
        }
    }
}