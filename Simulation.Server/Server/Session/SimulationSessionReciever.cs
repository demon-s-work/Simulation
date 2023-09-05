using com.simulation.contract;
using com.simulation.contract.FBE;

namespace Simulation.Server.Server.Session
{
    public class SimulationSessionReciever : Receiver, IReceiverListener
    {
        public SimulationSession Session { get; }

        public SimulationSessionReciever(SimulationSession session) { Session = session;}

        public void OnReceive(SimpleMessage request) { Session.OnReceive(request); }
        
        public void OnReceive(LoginMessage request) {Session.OnReceive(request);}
    }
}