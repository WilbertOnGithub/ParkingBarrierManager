using System.Diagnostics.CodeAnalysis;

namespace Arentheym.ParkingBarrier.Infrastructure.Database;

public class DatabaseConfiguration
{
    /// <summary>
    /// Use <see cref="ExpandedConnectionString"/> instead.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    public string ExpandedConnectionString =>
        ConnectionString.Replace("%APPDATA%",
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StringComparison.InvariantCulture);
}