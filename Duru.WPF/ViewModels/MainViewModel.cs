// MainViewModel.cs
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using Duru.Core.ViewModels;

namespace Duru.WPF.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private ReactiveObject _currentViewModel;

        public ReactiveObject CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        public ReactiveCommand<Unit, ReactiveObject> NavigateDashboardCommand { get; }
        public ReactiveCommand<Unit, ReactiveObject> NavigateEmployeesCommand { get; }
        public ReactiveCommand<Unit, ReactiveObject> LogoutCommand { get; }

        private readonly LoginViewModel _loginViewModel;
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly EmployeeManagementViewModel _employeeViewModel;

        public MainViewModel(
            LoginViewModel loginViewModel,
            DashboardViewModel dashboardViewModel,
            EmployeeManagementViewModel employeeViewModel)
        {
            _loginViewModel = loginViewModel;
            _dashboardViewModel = dashboardViewModel;
            _employeeViewModel = employeeViewModel;

            // Set login as initial view
            CurrentViewModel = _loginViewModel;

            // Navigation commands
            NavigateDashboardCommand = ReactiveCommand.Create(() => CurrentViewModel = _dashboardViewModel);
            NavigateEmployeesCommand = ReactiveCommand.Create(() => CurrentViewModel = _employeeViewModel);
            LogoutCommand = ReactiveCommand.Create(() => CurrentViewModel = _loginViewModel);

            // Handle successful login
            _loginViewModel.LoginCommand
                .Where(success => success)
                .Subscribe(_ => CurrentViewModel = _dashboardViewModel);
        }
    }
}