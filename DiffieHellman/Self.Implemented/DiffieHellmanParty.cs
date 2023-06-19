using System.Numerics;
using System.Security.Cryptography;

namespace DiffieHellman.Self.Implemented
{
    public class DiffieHellmanParty
    {
        private BigInteger Prime { get; }
        private BigInteger Generator { get; }
        private BigInteger PrivateKey { get; init; }
        public BigInteger PublicKey { get; private set; }
        public BigInteger SharedSecret { get; private set; }

        public DiffieHellmanParty()
        {
            Prime = new BigInteger(65537);
            Generator = new BigInteger(3);
            
            PrivateKey = GenerateRandomPrivateKey(Prime - 2);
            PublicKey = BigInteger.ModPow(Generator, PrivateKey, Prime);
        }

        public void ComputeSharedSecret(BigInteger otherPublicKey)
        {
            SharedSecret = BigInteger.ModPow(otherPublicKey, PrivateKey, Prime);
        }

        private BigInteger GenerateRandomPrivateKey(BigInteger max)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[32];
                rng.GetBytes(bytes);

                // Побітове AND з 01111111
                bytes[^1] &= 0x7F;

                var privateKey = new BigInteger(bytes) % max;
                return privateKey;
            }
        }
    }
}
