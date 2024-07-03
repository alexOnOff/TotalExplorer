using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace TotalExplorer.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();


    }

    private void CloseButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if(Application.Current == null) return;

        if(Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            desktopLifetime.Shutdown();
        }
    }

    private void CollapseButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (Parent.Parent is MainWindow window)
            window.WindowState = WindowState.Minimized;
    }

    private void ExpandButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (Parent.Parent is MainWindow window)
        {
            if (window.WindowState == WindowState.Normal)
                window.WindowState = WindowState.Maximized;
            else if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
        }
    }

    private void Binding_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
    }
}
