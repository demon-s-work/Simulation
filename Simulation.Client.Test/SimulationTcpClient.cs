using System.Net.Sockets;
using TcpClient=NetCoreServer.TcpClient;

namespace Simulation.Client.Test
{
    public class SimulationTcpClient : TcpClient
    {
        private bool _disposed;
        private Timer _reconnectTimer;
        
        public delegate void DisconnectedHandler();
        public event DisconnectedHandler Disconnected = () => {};
        
        public delegate void ConnectedHandler();
        public event ConnectedHandler Connected = () => {};
        
        public delegate void ReceivedHandler(byte[] buffer, long offset, long size);
        public event ReceivedHandler Received = (buffer, offset, size) => {};

        public SimulationTcpClient(string address, int port) : base(address, port) {}
        
        public bool ConnectAndStart()
        {
            Console.WriteLine($"TCP protocol client starting a new session with Id '{Id}'...");

            StartReconnectTimer();
            return ConnectAsync();
        }

        public bool DisconnectAndStop()
        {
            Console.WriteLine($"TCP protocol client stopping the session with Id '{Id}'...");

            StopReconnectTimer();
            DisconnectAsync();
            return true;
        }
        
        public override bool Reconnect()
        {
            return ReconnectAsync();
        }
        
        public void StartReconnectTimer()
        {
            // Start the reconnect timer
            _reconnectTimer = new Timer(state =>
            {
                Console.WriteLine($"TCP reconnect timer connecting the client session with Id '{Id}'...");
                ConnectAsync();
            }, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        public void StopReconnectTimer()
        {
            // Stop the reconnect timer
            _reconnectTimer?.Dispose();
            _reconnectTimer = null;
        }
        
        protected override void OnConnected()
        {
            Console.WriteLine($"TCP protocol client connected a new session with Id '{Id}' to remote address '{Address}' and port {Port}");

            Connected?.Invoke();
        }
        
        protected override void OnDisconnected()
        {
            Console.WriteLine($"TCP protocol client disconnected the session with Id '{Id}'");

            // Setup and asynchronously wait for the reconnect timer
            _reconnectTimer?.Change(TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);

            Disconnected?.Invoke();
        }
        
        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            Received?.Invoke(buffer, offset, size);
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"TCP protocol client caught a socket error: {error}");
        }

        protected override void Dispose(bool disposingManagedResources)
        {
            if (!_disposed)
            {
                if (disposingManagedResources)
                {
                    // Dispose managed resources here...
                    StopReconnectTimer();
                }

                // Dispose unmanaged resources here...

                // Set large fields to null here...

                // Mark as disposed.
                _disposed = true;
            }

            // Call Dispose in the base class.
            base.Dispose(disposingManagedResources);
        }
    }
}