using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Duru.App.Services;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Duru.App.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private bool _rememberMe;



        public LoginViewModel(IAuthService authService, INavigationService navigationService)
        {
            Title = "Duru Hotel Management - Login";
            _authService = authService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Username and password are required.";
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var result = await _authService.LoginAsync(Username, Password);

                if (result)
                {
                    // Navigate to Dashboard on successful login
                    await _navigationService.NavigateToAsync("///dashboard");
                }
                else
                {
                    ErrorMessage = "Invalid username or password.";
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void ForgotPassword()
        {
            // Implement password recovery logic
        }
    }
}