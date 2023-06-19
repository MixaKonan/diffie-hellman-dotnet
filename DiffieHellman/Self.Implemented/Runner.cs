namespace DiffieHellman.Self.Implemented;

public static class Runner
{
    public static void Run()
    {
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine("\n\n\nRunning self-implemented Diffie-Hellman algorithm...");

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Creating parties...");

        var alice = new DiffieHellmanParty();
        var bob = new DiffieHellmanParty();

        Console.WriteLine("Exchanging public keys...");
        alice.ComputeSharedSecret(bob.PublicKey);
        bob.ComputeSharedSecret(alice.PublicKey);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Verifying shared secret equality...");
        
        if (alice.SharedSecret.Equals(bob.SharedSecret))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Equality confirmed. Now the shared secret might be used in symmetric cryptography.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Equality not confirmed.");
        }
    }
}