using System;
using System.Collections.ObjectModel;

using Arentheym.ParkingBarrier.UI.Messages;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class LogViewModel : ObservableObject
{
    public ObservableCollection<LogMessage> LogEntries { get; } = new();

    public LogViewModel()
    {
        WeakReferenceMessenger.Default.Register<LogEntryAdded>(this, OnMessageReceived);
    }

    private void OnMessageReceived(object recipient, LogEntryAdded message)
    {
        LogEntries.Add(message.Value);
    }
}
