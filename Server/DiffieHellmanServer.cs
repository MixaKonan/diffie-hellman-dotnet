using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace Server;

public class DiffieHellmanServer : TcpServer
{
    public DiffieHellmanServer(IPAddress address, int port) : base(address, port) {}

    protected override TcpSession CreateSession() { return new DiffieHellmanSession(this); }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP server caught an error with code {error}");
    }

    public void SendPublicKey()
    {
        var (id, session) = this.Sessions.First();
        var diffieHellmanSession = (DiffieHellmanSession)session;
        diffieHellmanSession.SendPublicKey();
    }
}