using System.Security.Cryptography;
using System.Text;

namespace Arentheym.ParkingBarrier.Application;

public class Encryptor
{
    public string Encrypt (string toEncrypt)
    {
        using var aes = Aes.Create();

        aes.Key = key;
        byte[] iv = aes.IV;

        MemoryStream outputStream = new MemoryStream();
        outputStream.Write(iv, 0, iv.Length);

        using CryptoStream cryptoStream =
            new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter encryptWriter = new StreamWriter(cryptoStream);
        encryptWriter.Write(toEncrypt);

        outputStream.Seek(9, SeekOrigin.Begin);
        using StreamReader reader = new StreamReader(outputStream);
        string text = reader.ReadToEnd();

        byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(text);
        return Convert.ToBase64String(toEncodeAsBytes);
    }

    private byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05 };
}