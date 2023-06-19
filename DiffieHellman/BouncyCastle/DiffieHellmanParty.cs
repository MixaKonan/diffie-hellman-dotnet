using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace DiffieHellman.BouncyCastle;

public class DiffieHellmanParty
{
    public DHParameters Parameters { get; }

    public byte[] SharedSecret { get; private set; }

    public AsymmetricKeyParameter PublicKey => _keyPair.Public;
    
    private readonly AsymmetricCipherKeyPair _keyPair;

    public DiffieHellmanParty(DHParameters publicParameters)
    {
        Parameters = publicParameters;

        var keyPairGenerator = new DHKeyPairGenerator();
        var keyGenerationParameters = new DHKeyGenerationParameters(new SecureRandom(), Parameters);
        keyPairGenerator.Init(keyGenerationParameters);

        _keyPair = keyPairGenerator.GenerateKeyPair();
    }

    public void ComputeSharedSecret(AsymmetricKeyParameter otherPartyPublicKey)
    {
        var agreement = new DHBasicAgreement();
        agreement.Init(_keyPair.Private);

        SharedSecret = agreement.CalculateAgreement(otherPartyPublicKey).ToByteArrayUnsigned();
    }
}