using com.simulation.contract;
using NetCoreServer;
namespace Simulation.Server.Server.Session
{
    public class SimulationSession : TcpSession
    {
        public string Name { get; set; }
        public string DisplayName => Name ?? Id.ToString();
        public SimulationSessionSender Sender { get; }
        public SimulationSessionReciever Receiver { get; }
        public SimulationSession(TcpServer server) : base(server)
        {
            Sender = new SimulationSessionSender(this);
            Receiver = new SimulationSessionReciever(this);
        }

        public void OnReceive(LoginMessage message)
        {
            Logger.Log(this, $"|[{DisplayName}]:{message.Login}");
            Name = message.Login;
        }
        
        public void OnReceive(SimpleMessage message)
        {
            Logger.Log(this, $"|[{DisplayName}]:{message.Message}");
            (Server as SimulationServer).Sender.Send(new SimpleMessage($"|[{DisplayName}]:{message.Message}"));
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            Receiver.Receive(buffer, offset, size);
        }

        protected override void OnConnected()
        {
            Logger.Log(this, $"{DisplayName} connected", ConsoleColor.Gray);
        }

        protected override void OnDisconnected()
        {
            base.OnDisconnected();
        }
    }
}