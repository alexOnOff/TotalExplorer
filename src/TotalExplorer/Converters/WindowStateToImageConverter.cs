using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;

namespace TotalExplorer.Converters;

internal class WindowStateToImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var bitmap = new Bitmap(AssetLoader.Open(new Uri("avares://TotalExplorer/Assets/window-maximize.png")));

        if (value is WindowState windowState)
        {
            if(windowState == WindowState.Maximized)
                return new Bitmap(AssetLoader.Open(new Uri("avares://TotalExplorer/Assets/window-restore.png")));
        }

        return bitmap;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
