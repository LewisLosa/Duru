// EmployeeManagementViewModel.cs
using Duru.Core.Models;
using Duru.Core.Services;
using Duru.Core.ViewModels;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace Duru.WPF.ViewModels
{
    public class EmployeeManagementViewModel : ReactiveObject
    {
        private readonly IEmployeeService _employeeService;
        private EmployeeViewModel _selectedEmployee;
        private bool _isLoading;
        private string _searchTerm = string.Empty;
        private bool _isEditing;

        public EmployeeViewModel SelectedEmployee
        {
            get => _selectedEmployee;
            set => this.RaiseAndSetIfChanged(ref _selectedEmployee, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
        }

        public bool IsEditing
        {
            get => _isEditing;
            set => this.RaiseAndSetIfChanged(ref _isEditing, value);
        }

        public ObservableCollection<EmployeeViewModel> Employees { get; } = new();

        public ReactiveCommand<Unit, Unit> LoadEmployeesCommand { get; }
        public ReactiveCommand<Unit, Unit> AddEmployeeCommand { get; }
        public ReactiveCommand<Unit, bool> EditEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelEditCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteEmployeeCommand { get; }

        public EmployeeManagementViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            // Initialize commands
            LoadEmployeesCommand = ReactiveCommand.CreateFromTask(LoadEmployeesAsync);

            AddEmployeeCommand = ReactiveCommand.Create(() =>
            {
                SelectedEmployee = new EmployeeViewModel
                {
                    Status = "Active",
                    CreatedAt = DateTime.Now,
                    HireDate = DateTime.Now
                };
                IsEditing = true;
            });

            var canEdit = this.WhenAnyValue(
                x => x.SelectedEmployee,
                (EmployeeViewModel employee) => employee != null
            );


            EditEmployeeCommand = ReactiveCommand.Create(
                () => IsEditing = true,
                canEdit
            );
            
            var canSave = this.WhenAnyValue(
                x => x.IsEditing,
                x => x.SelectedEmployee,
                (isEditing, employee) => isEditing && employee != null &&
                                        !string.IsNullOrWhiteSpace(employee.FirstName) &&
                                        !string.IsNullOrWhiteSpace(employee.LastName)
            );

            SaveEmployeeCommand = ReactiveCommand.CreateFromTask(
                SaveEmployeeAsync,
                canSave
            );

            CancelEditCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsEditing = false;
                await LoadEmployeesAsync();
            });
            DeleteEmployeeCommand = ReactiveCommand.CreateFromTask(
                DeleteEmployeeAsync,
                canEdit
            );

            // Load data initially
            LoadEmployeesCommand.Execute().Subscribe();

            this.WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(async _ => await LoadEmployeesAsync());
        }

        private async Task LoadEmployeesAsync()
        {
            IsLoading = true;

            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                Employees.Clear();

                // Apply search filter if there's a search term
                var filteredEmployees = string.IsNullOrWhiteSpace(SearchTerm)
                    ? employees
                    : employees.Where(e =>
                        e.FirstName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                        e.LastName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                        e.Position.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));

                foreach (var employee in filteredEmployees)
                {
                    Employees.Add(employee.ToViewModel());
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SaveEmployeeAsync()
        {
            IsLoading = true;

            try
            {
                if (SelectedEmployee.EmployeeId == 0)
                {
                    // Create new employee
                    var model = SelectedEmployee.ToModel();
                    int id = await _employeeService.CreateEmployeeAsync(model);

                    if (id > 0)
                    {
                        SelectedEmployee.EmployeeId = id;
                        Employees.Add(SelectedEmployee);
                    }
                }
                else
                {
                    // Update existing employee
                    var model = SelectedEmployee.ToModel();
                    await _employeeService.UpdateEmployeeAsync(model);

                    // Update in the list
                    var index = Employees.IndexOf(Employees.FirstOrDefault(e => e.EmployeeId == SelectedEmployee.EmployeeId));
                    if (index >= 0)
                    {
                        Employees[index] = SelectedEmployee;
                    }
                }

                IsEditing = false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task DeleteEmployeeAsync()
        {
            if (SelectedEmployee == null)
                return;

            IsLoading = true;

            try
            {
                await _employeeService.DeleteEmployeeAsync(SelectedEmployee.EmployeeId);
                Employees.Remove(SelectedEmployee);
                SelectedEmployee = null;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}