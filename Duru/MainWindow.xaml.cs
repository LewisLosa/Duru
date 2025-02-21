using Duru.Views;
using System.Windows;

namespace Duru;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.NavigationService.Navigate(new LoginView());
    }
}