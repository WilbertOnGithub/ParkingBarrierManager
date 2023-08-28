using CommandLine;

namespace Arentheym.ParkingBarrier.UI;

public static class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                Console.WriteLine(o.StringToDecrypt);
                Console.WriteLine(o.StringToEncrypt);
            });
    }
}