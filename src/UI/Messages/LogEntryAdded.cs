using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Arentheym.ParkingBarrier.UI.Messages;

public class LogEntryAdded(LogMessage value) : ValueChangedMessage<LogMessage>(value);
