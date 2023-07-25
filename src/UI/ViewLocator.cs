using System;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Arentheym.ParkingBarrier.UI;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        if (data != null)
        {
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        return new TextBlock { Text = "data was null" };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}