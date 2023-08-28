using CommandLine;
using CommandLine.Text;

namespace Arentheym.ParkingBarrier.UI;

public class Options
{
    [Option('e', "encrypt", Required = false, HelpText = "string to encrypt.", SetName = "encrypt")]
    public string StringToEncrypt { get; set; } = string.Empty;

    [Option('d', "decrypt", Required = false, HelpText = "string to decrypt.", SetName = "decrypt")]
    public string StringToDecrypt { get; set; } = string.Empty;

    [Usage(ApplicationAlias = "ApiKeyEncryption")]
    public static IEnumerable<Example> Examples =>
        new List<Example>() {
            new Example("Encrypt API key", new Options { StringToEncrypt = "thiswouldbethesecretkey" }),
            new Example("Decrypt API key", new Options { StringToDecrypt = "encrypted key here" })
        };
}