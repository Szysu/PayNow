using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace PayNow.Views;

public partial class Dialog : UserControl
{
    public static Dialog Instance { get; private set; }
    
    private Timer _cleanupTimer;

    public Dialog()
    {
        Instance = this;
        InitializeComponent();
    }

    public void AddError(string message)
    {
        TextBlock.Text = message;
        TextBlock.Foreground = Brushes.Red;
        _cleanupTimer = new(_ => Reset(), null, 2000, Timeout.Infinite);
    }

    public void AddSuccess(string message)
    {
        TextBlock.Text = message;
        TextBlock.Foreground = Brushes.Green;
        _cleanupTimer = new(_ => Reset(), null, 2000, Timeout.Infinite);
    }

    private void Reset()
    {
        Dispatcher.UIThread.Invoke(() => TextBlock.Text = "");
    }
}