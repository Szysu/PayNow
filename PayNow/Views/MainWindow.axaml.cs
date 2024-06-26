using Avalonia.ReactiveUI;
using PayNow.ViewModels;

namespace PayNow.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}