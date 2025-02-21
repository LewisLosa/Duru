using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Duru.Views;
using System.Security;
using System.Windows;

namespace Duru.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        private bool isSignInVisible = true;

        public Visibility SignInVisibility => IsSignInVisible ? Visibility.Visible : Visibility.Hidden;
        public Visibility SignUpVisibility => IsSignInVisible ? Visibility.Hidden : Visibility.Visible;

        [ObservableProperty]
        private string username;
        public SecureString SecurePassword { private get; set; }

        [RelayCommand]
        private void Login()
        {
            // TODO: Buraya giriş kontrolü eklenebilir.
            if (!string.IsNullOrEmpty(Username) && SecurePassword != null)
            {
                // Ana pencereyi bul ve yönlendir
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new MainView());
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz!");
            }
        }

        private void Register()
        {
            // TODO: Buraya kayıt kontrolü eklenebilir.

        }

        [RelayCommand]
        private void TogglePanel()
        {
            IsSignInVisible = !IsSignInVisible;
            OnPropertyChanged(nameof(SignInVisibility));
            OnPropertyChanged(nameof(SignUpVisibility));
        }
    }
}
