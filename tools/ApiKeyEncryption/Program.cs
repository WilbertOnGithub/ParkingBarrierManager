using Arentheym.ParkingBarrier.Application;
using CommandLine;

namespace Arentheym.ParkingBarrier.UI;

internal static class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                var encryptor = new Encryptor();

                if (!string.IsNullOrEmpty(options.StringToEncrypt))
                {
                    Console.WriteLine($"Encrypted: {encryptor.Encrypt(options.StringToEncrypt)}");
                }

                if (!string.IsNullOrEmpty(options.StringToDecrypt))
                {
                    Console.WriteLine($"Decrypted: {encryptor.Decrypt(options.StringToDecrypt)}");
                }
            });
    }
}