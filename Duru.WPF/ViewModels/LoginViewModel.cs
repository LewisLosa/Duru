// LoginViewModel.cs
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Duru.WPF.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private bool _isLoading;
        private string _errorMessage = string.Empty;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        public ReactiveCommand<Unit, bool> LoginCommand { get; }

        public LoginViewModel()
        {
            var canLogin = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password)
            );

            LoginCommand = ReactiveCommand.CreateFromTask(
                ExecuteLoginAsync,
                canLogin
            );

            LoginCommand.ThrownExceptions
                .Subscribe(ex => ErrorMessage = ex.Message);
        }

        private async Task<bool> ExecuteLoginAsync()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                // Simulate authentication delay
                await Task.Delay(1000);

                // TODO: Implement actual authentication with service
                if (Username == "admin" && Password == "admin")
                {
                    return true;
                }

                ErrorMessage = "Invalid username or password";
                return false;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
