using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Duru.App.Services;
using Duru.Core.Services;
using Duru.Core.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Duru.App.ViewModels
{
    public partial class EmployeeManagementViewModel : ViewModelBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private ObservableCollection<EmployeeViewModel> _employees = new();

        [ObservableProperty]
        private EmployeeViewModel _selectedEmployee;

        [ObservableProperty]
        private EmployeeViewModel _newEmployee = new();

        [ObservableProperty]
        private bool _isEditMode;

        [ObservableProperty]
        private bool _isAddMode;

        [ObservableProperty]
        private string _searchQuery = string.Empty;

        [ObservableProperty]
        private ObservableCollection<string> _positions = new ObservableCollection<string>
        {
            "Manager", "Receptionist", "Housekeeping", "Maintenance", "Chef", "Waiter"
        };

        public EmployeeManagementViewModel(IEmployeeService employeeService, IDialogService dialogService)
        {
            Title = "Employee Management";
            _employeeService = employeeService;
            _dialogService = dialogService;
            SelectedEmployee = new EmployeeViewModel();
        }

        [RelayCommand]
        private async Task LoadEmployeesAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var employeeList = await _employeeService.GetAllEmployeesAsync();

                Employees.Clear();
                foreach (var employee in employeeList)
                {
                    Employees.Add(employee.ToViewModel());
                }

                if (!string.IsNullOrWhiteSpace(SearchQuery))
                {
                    FilterEmployees();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load employees: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void FilterEmployees()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                LoadEmployeesCommand.Execute(null);
                return;
            }

            var filteredEmployees = Employees.Where(e =>
                e.FirstName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                e.LastName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                e.Position.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

            Employees.Clear();
            foreach (var employee in filteredEmployees)
            {
                Employees.Add(employee);
            }
        }

        [RelayCommand]
        private void AddEmployee()
        {
            IsAddMode = true;
            IsEditMode = false;
            NewEmployee = new EmployeeViewModel
            {
                HireDate = DateTime.Today,
                Status = "Active",
                CreatedAt = DateTime.Now
            };
        }

        [RelayCommand]
        private void EditEmployee()
        {
            if (SelectedEmployee == null || SelectedEmployee.EmployeeId == 0)
            {
                _dialogService.ShowMessage("Please select an employee to edit.");
                return;
            }

            IsEditMode = true;
            IsAddMode = false;
        }

        [RelayCommand]
        private async Task SaveEmployeeAsync()
        {
            if (IsAddMode)
            {
                // Validate employee data
                if (string.IsNullOrWhiteSpace(NewEmployee.FirstName) ||
                    string.IsNullOrWhiteSpace(NewEmployee.LastName))
                {
                    ErrorMessage = "First name and last name are required.";
                    return;
                }

                IsBusy = true;
                try
                {
                    var model = NewEmployee.ToModel();
                    int id = await _employeeService.CreateEmployeeAsync(model);
                    if (id > 0)
                    {
                        NewEmployee.EmployeeId = id;
                        Employees.Add(NewEmployee);
                        IsAddMode = false;
                        _dialogService.ShowMessage("Employee added successfully.");
                    }
                    else
                    {
                        ErrorMessage = "Failed to add employee.";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error adding employee: {ex.Message}";
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else if (IsEditMode)
            {
                // Validate employee data
                if (string.IsNullOrWhiteSpace(SelectedEmployee.FirstName) ||
                    string.IsNullOrWhiteSpace(SelectedEmployee.LastName))
                {
                    ErrorMessage = "First name and last name are required.";
                    return;
                }

                IsBusy = true;
                try
                {
                    var model = SelectedEmployee.ToModel();
                    bool success = await _employeeService.UpdateEmployeeAsync(model);
                    if (success)
                    {
                        IsEditMode = false;
                        _dialogService.ShowMessage("Employee updated successfully.");
                    }
                    else
                    {
                        ErrorMessage = "Failed to update employee.";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error updating employee: {ex.Message}";
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        private async Task DeleteEmployeeAsync()
        {
            if (SelectedEmployee == null || SelectedEmployee.EmployeeId == 0)
            {
                _dialogService.ShowMessage("Please select an employee to delete.");
                return;
            }

            bool confirmed = await _dialogService.ShowConfirmationAsync(
                "Delete Employee",
                $"Are you sure you want to delete {SelectedEmployee.FullName}?");

            if (!confirmed)
                return;

            IsBusy = true;
            try
            {
                bool success = await _employeeService.DeleteEmployeeAsync(SelectedEmployee.EmployeeId);
                if (success)
                {
                    Employees.Remove(SelectedEmployee);
                    SelectedEmployee = new EmployeeViewModel();
                    _dialogService.ShowMessage("Employee deleted successfully.");
                }
                else
                {
                    ErrorMessage = "Failed to delete employee.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting employee: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            IsAddMode = false;
            IsEditMode = false;
            NewEmployee = new EmployeeViewModel();
        }
    }
}