using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using Duru.Views;

namespace Duru.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public StatusBarViewModel StatusBarVM => StatusBarViewModel.Instance;
        [RelayCommand]
        private void NavigateReservations()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new ReservationsView());
            StatusBarVM.StatusText = "🔁 Rezervasyonlar yüklendi. (128ms)";
        }

        [RelayCommand]
        private void Logout()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.Navigate(new LoginView());
            StatusBarVM.StatusText = "👤 Hesaptan çıkış yapıldı.";
        }

    }
}
