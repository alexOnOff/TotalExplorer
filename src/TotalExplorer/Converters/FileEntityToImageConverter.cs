using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;
using TotalExplorer.ViewModels.FileSystem;

namespace TotalExplorer.Converters;

public class FileEntityToImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var bitmap = new Bitmap(AssetLoader.Open(new Uri("avares://TotalExplorer/Assets/directory-solid.png")));

        if (value is FileViewModel)
        {
            return new Bitmap(AssetLoader.Open(new Uri("avares://TotalExplorer/Assets/file.png")));
        }

        return bitmap;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
