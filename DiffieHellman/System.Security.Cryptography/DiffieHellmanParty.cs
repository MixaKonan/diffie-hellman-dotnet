using System.Security.Cryptography;

namespace DiffieHellman.System.Security.Cryptography;

public class DiffieHellmanParty : IDisposable
{
    public byte[] PublicKey { get; }
    public byte[] SharedSecret { get; private set; }

    private readonly ECDiffieHellmanCng _ecDiffieHellmanCng;

    public DiffieHellmanParty()
    {
        _ecDiffieHellmanCng = new ECDiffieHellmanCng();
        _ecDiffieHellmanCng.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
        _ecDiffieHellmanCng.HashAlgorithm = CngAlgorithm.Sha256;

        PublicKey = _ecDiffieHellmanCng.PublicKey.ToByteArray();
    }

    public void SetSharedSecret(byte[] otherPartyPublicKey)
    {
        SharedSecret = GetCngKey(otherPartyPublicKey);
    }

    private byte[] GetCngKey(byte[] key)
    {
        var cngPublicKey = CngKey.Import(key, CngKeyBlobFormat.EccPublicBlob);

        return _ecDiffieHellmanCng.DeriveKeyMaterial(cngPublicKey);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _ecDiffieHellmanCng.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~DiffieHellmanParty()
    {
        Dispose(false);
    }
}