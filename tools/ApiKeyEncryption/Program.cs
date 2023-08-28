using CommandLine;

namespace Arentheym.ParkingBarrier.UI;

internal static class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                Console.WriteLine($"string to decrypt {options.StringToDecrypt}");
                Console.WriteLine(options.StringToEncrypt);
            });
    }
}