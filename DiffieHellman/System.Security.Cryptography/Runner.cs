namespace DiffieHellman.System.Security.Cryptography;

public static class Runner
{
    public static void Run()
    {
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine("\n\n\nRunning Diffie-Hellman algorithm using System.Security.Cryptography...");

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Creating parties...");

        using var alice = new DiffieHellmanParty();
        using var bob = new DiffieHellmanParty();

        Console.WriteLine("Exchanging public keys...");
        alice.SetSharedSecret(bob.PublicKey);
        bob.SetSharedSecret(alice.PublicKey);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Verifying shared secret equality...");

        if (alice.SharedSecret.SequenceEqual(bob.SharedSecret))
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