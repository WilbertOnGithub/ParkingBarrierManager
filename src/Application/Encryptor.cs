using System.Security.Cryptography;
using System.Text;

namespace Arentheym.ParkingBarrier.Application;

public sealed class Encryptor : IDisposable
{
    private readonly Aes aes;
    private const int keyLength = 16;
    private const int initializationVectorLength = keyLength;

    public Encryptor()
    {
        aes = Aes.Create();

        // Size is number of chars multiplied by 2 (2 bytes per char) multiplied by 8 (8 bits per byte) = 256 bits.
        aes.KeySize = keyLength * 2 * 8;
        aes.Key = GetPassPhrase();
        aes.GenerateIV();
    }

    public string Encrypt(string plainText)
    {
        byte[] encrypted;

        using (var ms = new MemoryStream())
        {
            // Prepend the generated initialization vector to the encrypted bytes.
            ms.Write(aes.IV, 0, initializationVectorLength);
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

    public string Decrypt(string encryptedText)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using (var ms = new MemoryStream(encryptedBytes))
        {
            // Read initialization vector from the beginning of the stream.
            byte[] buffer = new byte[initializationVectorLength];
            ms.Read(buffer, 0, initializationVectorLength);

            // Set the correct initialization vector.
            aes.IV = buffer;

            using (var decryptor = aes.CreateDecryptor())
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
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