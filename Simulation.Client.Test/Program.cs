using com.simulation.contract;
namespace Simulation.Client.Test
{
    public static class Program
    {
        public static void Main()
        {
            string address = "127.0.0.1";
            int port = 4444;

            var client = new SimulationClient(address, port);
            client.ReceivedNotify_SimpleMessage += response => {
                Console.WriteLine(response.Message);
            };
            client.ConnectAndStart();

            client.Send(new LoginMessage(Console.ReadLine()));
            while (true)
            {
                var msg = Console.ReadLine();
                if (msg is not null)
                {
                    client.Send(new SimpleMessage(msg));
                }
            }
        }
    }
}