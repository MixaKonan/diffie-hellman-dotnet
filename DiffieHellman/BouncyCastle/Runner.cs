using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace DiffieHellman.BouncyCastle;

public static class Runner
{
    public static void Run()
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("\n\n\nRunning Diffie-Hellman algorithm using BouncyCastle.Crypto...");

        // Створення публічних параметрів p, g
        var secureRandom = new SecureRandom();
        var parametersGenerator = new DHParametersGenerator();
        parametersGenerator.Init(256, 100, secureRandom);

        var parameters = parametersGenerator.GenerateParameters();

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Creating parties...");

        // Ініціалізація потрібних даних кожної сторони:
        // публічного та приватного ключів
        var alice = new DiffieHellmanParty(parameters);
        var bob = new DiffieHellmanParty(parameters);

        Console.WriteLine("Exchanging public keys...");

        alice.ComputeSharedSecret(bob.PublicKey);
        bob.ComputeSharedSecret(alice.PublicKey);

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