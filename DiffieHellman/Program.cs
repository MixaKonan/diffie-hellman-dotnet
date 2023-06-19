namespace DiffieHellman;

public static class Program
{
    public static void Main()
    {
        System.Security.Cryptography.Runner.Run();
        Self.Implemented.Runner.Run();
        BouncyCastle.Runner.Run();
    }
}