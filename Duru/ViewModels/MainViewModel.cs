using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using Duru.Views;

namespace Duru.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public StatusBarViewModel StatusBarVM { get; } = new StatusBarViewModel();
        [RelayCommand]
        private void NavigateReservations()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ReservationsView());
            StatusBarVM.StatusText = "Sayfa 1: Sadece yazı var";
            StatusBarVM.StatusButtonVisibility = System.Windows.Visibility.Collapsed;
        }

        [RelayCommand]
        private void Logout()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new LoginView());
            StatusBarVM.StatusText = "Sayfa 2: Yazı ve buton var";
            StatusBarVM.StatusButtonVisibility = System.Windows.Visibility.Visible;
        }

    }
}
