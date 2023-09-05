using System.Net;
using Simulation.Server.Server;
namespace Simulation.Server
{
    public class ServerManager
    {
        private SimulationServer Server;
        public void StartServer()
        {
            int port = 4444;
            Server = new SimulationServer(IPAddress.Any, port);
            Server.Start();
        }

        public void SetOutput(EventHandler<LogEvent> outputEventHandler)
        {
            Logger.OnMessage += outputEventHandler;
        }
    }
}