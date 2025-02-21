using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Duru.ViewModels
{
    public partial class StatusBarViewModel : ObservableObject
    {

        /* TODO: StatusBar güncellenmiyor. Bunun için ne yapacağım hakkında bir fikrim yok. */
        [ObservableProperty]
        private string statusText = "Varsayılan mesaj";

        [ObservableProperty]
        private Visibility statusButtonVisibility = Visibility.Collapsed;

        public StatusBarViewModel()
        {
            StatusButtonVisibility = Visibility.Collapsed;
        }


        [RelayCommand]
        private void StatusButtonClicked()
        {
            StatusText = "Button''a tıklandı.";
        }
    }
}
