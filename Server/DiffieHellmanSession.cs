using System.Numerics;
using System.Text;
using DiffieHellman.Self.Implemented;
using NetCoreServer;

namespace Server;

public class DiffieHellmanSession : TcpSession
{
    private readonly DiffieHellmanParty _party;

    public DiffieHellmanSession(TcpServer server)
        : base(server)
    {
        _party = new DiffieHellmanParty();
    }

    protected override void OnConnected()
    {
        Console.WriteLine("[SERVER] | Got a new connection.");
    }
    
    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        var message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        Console.WriteLine("[SERVER] | Incoming client's message: " + message);

        if (message.StartsWith("PBK:"))
        {
            ReceivePublicKey(message);
            
            Send($"PBK successfully received. Shared Secret computed.");

            Console.WriteLine($"[SERVER] | PBK successfully received. Shared Secret computed. {_party.SharedSecret}");
        }
    }
    
    public void SendPublicKey()
    {
        this.Send($"PBK:{_party.PublicKey}");
    }

    private void ReceivePublicKey(string message)
    {
        var pbk = message.Split(':')[1];
        var otherPartyPublicKey = BigInteger.Parse(pbk);
            
        _party.ComputeSharedSecret(otherPartyPublicKey);
    }
}