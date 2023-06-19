using System.Net;
using Client;

var client = new DiffieHellmanClient(IPAddress.Loopback, 7997);

client.Connect();

while (true)
{
    if (client.IsConnected)
    {
        client.ReceiveAsync();

        client.SendPublicKey();
    }
    
    Thread.Sleep(10000);
}

// var client = new DiffieHellmanClient(IPAddress.Loopback.ToString(), 7997);

// Console.Write("Client connecting...");
//
// client.Connect();
// while (!client.IsConnected)
// {
//     client.Reconnect();
// }
//
// Console.WriteLine("Done!");
//
// client.SendPublicKey();
//
// Console.ReadLine();