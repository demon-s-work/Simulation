using com.simulation.contract.FBE;

namespace Simulation.Server.Server
{
    public class SimulationSender : Sender, ISenderListener
    {
        public SimulationServer Server { get; }

        public SimulationSender(SimulationServer server) { Server = server; }

        public long OnSend(byte[] buffer, long offset, long size)
        {
            Server.Multicast(buffer, offset, size);
            return size;
        }
        
    }
}