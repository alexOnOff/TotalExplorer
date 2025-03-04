using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using System;

namespace TotalExplorer.Views;

public partial class MainView : UserControl
{
    private const string PartTitleBar = "PART_TitleBar";
    private Grid? _titleBar;
    private bool _isMenuVisible = false;
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

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (e.Source is null) return;

        if(e.Source.Equals(PART_TitleBar))
        {
            if (Parent.Parent is MainWindow window)
            {
                window.BeginMoveDrag(e);
            }
        }
    }

}
