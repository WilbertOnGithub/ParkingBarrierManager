using System;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Arentheym.ParkingBarrier.UI;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? param)
    {
        ArgumentNullException.ThrowIfNull(param);

        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
