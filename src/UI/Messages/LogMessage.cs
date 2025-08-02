using System;

namespace Arentheym.ParkingBarrier.UI.Messages;

public record LogMessage()
{
    public DateTime Timestamp { get; init; } = DateTime.Now;
    public string Message { get; init; } = string.Empty;
}
