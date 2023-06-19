using System.Net.Sockets;
using System.Numerics;
using System.Text;
using DiffieHellman.Self.Implemented;
using IPAddress = System.Net.IPAddress;
using TcpClient = NetCoreServer.TcpClient;

namespace Client;

public class DiffieHellmanClient : TcpClient
{
    private bool _stop;
    private readonly DiffieHellmanParty _party;

    public DiffieHellmanClient(IPAddress address, int port)
        : base(address, port)
    {
        _party = new DiffieHellmanParty();
    }
    
    public void DisconnectAndStop()
    {
        _stop = true;
        DisconnectAsync();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Chat TCP client connected a new session with Id {Id}");
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");

        Thread.Sleep(1000);

        if (!_stop)
            Connect();
    }

    public void SendPublicKey()
    {
        Send($"PBK:{_party.PublicKey}");
    }
    
    private void ReceivePublicKey(string message)
    {
        var pbk = message.Split(':')[1];
        var otherPartyPublicKey = BigInteger.Parse(pbk);
            
        _party.ComputeSharedSecret(otherPartyPublicKey);
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        var message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        Console.WriteLine("[CLIENT] | Incoming server's message: : " + message);
        
        if (message.StartsWith("PBK:"))
        {
            ReceivePublicKey(message);
            
            Send($"PBK successfully received. Shared Secret computed.");
            
            Console.WriteLine($"[CLIENT] | PBK successfully received. Shared Secret computed. {_party.SharedSecret}");
        }
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP client caught an error with code {error}");
    }
}