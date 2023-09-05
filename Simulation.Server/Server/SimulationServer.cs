using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using NetCoreServer;
using Simulation.Server.Server.Session;
namespace Simulation.Server.Server
{
    public class SimulationServer : TcpServer
    {
        public SimulationSender Sender { get; set; }

        public SimulationServer(IPAddress address, int port) : base(address, port)
        {
            Sender = new SimulationSender(this);
        }
        
        protected override TcpSession CreateSession()
        {
            return new SimulationSession(this);
        }
        
        protected override void OnStarted()
        {
            Logger.Log(this, "Server started", ConsoleColor.Green);
        }
        
        protected override void OnStopped()
        {
            Logger.Log(this, "Server stopped", ConsoleColor.Yellow);
        }
        
        protected override void OnError(SocketError error)
        {
            Logger.Log(this, $"Panic! {error}", ConsoleColor.Red);
        }
    }
}