using com.simulation.contract.FBE;

namespace Simulation.Client.Test
{
    public class SimulationClient : com.simulation.contract.FBE.Client, ISenderListener, IReceiverListener, IDisposable
    {
        private readonly SimulationTcpClient _simulationTcpClient;
        
        public Guid Id => _simulationTcpClient.Id;
        public bool IsConnected => _simulationTcpClient.IsConnected;
        
        private bool _watchdog;
        private Thread _watchdogThread;
        
        public delegate void ConnectedHandler();
        public event ConnectedHandler Connected = () => {};
        
        public delegate void DisconnectedHandler();
        public event DisconnectedHandler Disconnected = () => {};
        
        private bool _disposed;

        public SimulationClient(string address, int port)
        {
            _simulationTcpClient = new SimulationTcpClient(address, port);
            _simulationTcpClient.Received += SimulationTcpClientOnReceived;
        }
        private void SimulationTcpClientOnReceived(byte[] buffer, long offset, long size)
        {
            Receive(buffer, offset, size);
        }

        public bool ConnectAndStart() { return _simulationTcpClient.ConnectAndStart(); }
        public bool DisconnectAndStop() { return _simulationTcpClient.DisconnectAndStop(); }
        public bool Reconnect() { return _simulationTcpClient.Reconnect(); }
        
        public bool StartWatchdog()
        {
            if (_watchdog)
                return false;

            Console.WriteLine("Watchdog thread starting...");

            // Start the watchdog thread
            _watchdog = true;
            _watchdogThread = new Thread(WatchdogThread);

            Console.WriteLine("Watchdog thread started!");

            return true;
        }

        public bool StopWatchdog()
        {
            if (!_watchdog)
                return false;

            Console.WriteLine("Watchdog thread stopping...");

            // Stop the watchdog thread
            _watchdog = false;
            _watchdogThread.Join();

            Console.WriteLine("Watchdog thread stopped!");

            return true;
        }
        
        public static void WatchdogThread(object obj)
        {
            var instance = obj as SimulationClient;
            if (instance == null)
                return;

            try
            {
                // Watchdog loop...
                while (instance._watchdog)
                {
                    var utc = DateTime.UtcNow;

                    // Watchdog the client
                    instance.Watchdog(utc);

                    // Sleep for a while...
                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Config client watchdog thread terminated: {e}");
            }
        }
        
        private void OnConnected()
        {
            // Reset FBE protocol buffers
            Reset();

            Connected?.Invoke();
        }
        
        private void OnDisconnected()
        {
            Disconnected?.Invoke();
        }

        public long OnSend(byte[] buffer, long offset, long size)
        {
            return _simulationTcpClient.SendAsync(buffer, offset, size) ? size : 0;
        }

        public void OnReceived(byte[] buffer, long offset, long size)
        {
            Receive(buffer, offset, size);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        private void DisposeClient()
        {
            _simulationTcpClient.Dispose();
        }
        
        protected virtual void Dispose(bool disposingManagedResources)
        {
            // The idea here is that Dispose(Boolean) knows whether it is
            // being called to do explicit cleanup (the Boolean is true)
            // versus being called due to a garbage collection (the Boolean
            // is false). This distinction is useful because, when being
            // disposed explicitly, the Dispose(Boolean) method can safely
            // execute code using reference type fields that refer to other
            // objects knowing for sure that these other objects have not been
            // finalized or disposed of yet. When the Boolean is false,
            // the Dispose(Boolean) method should not execute code that
            // refer to reference type fields because those objects may
            // have already been finalized."

            if (!_disposed)
            {
                if (disposingManagedResources)
                {
                    // Dispose managed resources here...
                    DisposeClient();
                }

                // Dispose unmanaged resources here...

                // Set large fields to null here...

                // Mark as disposed.
                _disposed = true;
            }
        }
    }
}