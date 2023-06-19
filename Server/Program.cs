using System.Net;
using Server;

var tcpServer = new DiffieHellmanServer(IPAddress.Any, 7997);

tcpServer.Start();

while (true)
{
    if (tcpServer is { IsStarted: true, ConnectedSessions: > 0 })
    {
        tcpServer.SendPublicKey();
        
        Thread.Sleep(10000);
    }
    
    Thread.Sleep(1000);
}