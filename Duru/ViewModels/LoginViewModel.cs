using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Duru.Views;
using System.Security;
using System.Windows;

namespace Duru.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        public StatusBarViewModel StatusBarVM => StatusBarViewModel.Instance;

        [ObservableProperty]
        private string username;
        public SecureString SecurePassword { private get; set; }

        [RelayCommand]
        private void Login()
        {
            // TODO: Buraya giriş kontrolü eklenebilir.
            if (!string.IsNullOrEmpty(Username) && SecurePassword != null)
            {
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.NavigationService.Navigate(new MainView());
                StatusBarVM.StatusText = "✅ Başarıyla giriş yaptınız.";

            }
            else
            {
                StatusBarVM.StatusText = "❌ Kullanıcı adı ve şifre boş olamaz.";
            }
        }
    }
}
