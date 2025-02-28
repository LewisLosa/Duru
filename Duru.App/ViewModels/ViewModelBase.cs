using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Duru.App.ViewModels
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [RelayCommand]
        private void ClearError()
        {
            ErrorMessage = string.Empty;
        }
    }
}