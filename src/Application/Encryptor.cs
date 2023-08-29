using System.Security.Cryptography;
using System.Text;

namespace Arentheym.ParkingBarrier.Application;

public sealed class Encryptor : IDisposable
{
    private readonly Aes aes;
    private const int keyLength = 16;

    public Encryptor()
    {
        aes = Aes.Create();

        // Size is number of chars multiplied by 2 (2 bytes per char) multiplied by 8 (8 bits per byte) = 256 bits.
        aes.KeySize = keyLength * 2 * 8;
        aes.Key = GetPassPhrase();
        aes.GenerateIV();
    }

    public string Encrypt (string plainText)
    {
        byte[] encrypted;

        using (var ms = new MemoryStream())
        {
            // Prepend the generated initialization vector to the encrypted bytes.
            ms.Write(aes.IV, 0, 16); ;
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
            }

            encrypted = ms.ToArray();
        }

        return Convert.ToBase64String(encrypted);
    }

    private static byte[] GetPassPhrase()
    {
        var key = "TheQuickBrownFox";
        if (key.Length < keyLength)
        {
            key += new string('0', keyLength - key.Length);
        }

        if (key.Length > keyLength)
        {
            throw new InvalidOperationException("Key must be 16 characters long.");
        }

        return Encoding.UTF8.GetBytes(key);
   }

    public void Dispose()
    {
        aes.Dispose();
    }
}