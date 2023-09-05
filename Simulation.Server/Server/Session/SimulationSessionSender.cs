using com.simulation.contract.FBE;

namespace Simulation.Server.Server.Session
{
    public class SimulationSessionSender : Sender, ISenderListener
    {
        public SimulationSession Session { get; }

        public SimulationSessionSender(SimulationSession session) { Session = session; }
        
        public long OnSend(byte[] buffer, long offset, long size)
        {
            return Session.SendAsync(buffer, offset, size) ? size : 0;
        }
    }
}