using CommandLine;

namespace Arentheym.ParkingBarrier.UI;

public class Options
{
    [Option('e', "encrypt", Required = false, HelpText = "string to encrypt.", SetName = "encrypt")]
    public string StringToEncrypt { get; set; } = string.Empty;

    [Option('d', "decrypt", Required = false, HelpText = "string to decrypt.", SetName = "decrypt")]
    public string StringToDecrypt { get; set; } = string.Empty;
}