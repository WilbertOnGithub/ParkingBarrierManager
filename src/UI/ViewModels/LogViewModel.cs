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

        for (int i = 0; i < 30; i++)
        {
            LogEntries.Insert(0, new LogMessage(){ Timestamp = DateTime.Now, Message = "foo"});
        }
    }

    private void OnMessageReceived(object recipient, LogEntryAdded message)
    {
        throw new System.NotImplementedException();
    }
}
