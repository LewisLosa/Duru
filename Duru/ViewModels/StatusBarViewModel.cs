using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Duru.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    // Singleton Instance (Tüm uygulama boyunca tek bir nesne)
    private static StatusBarViewModel? _instance;
    public static StatusBarViewModel Instance => _instance ??= new StatusBarViewModel();

    [ObservableProperty] private string statusText = "✅ Program başlatıldı.";

    public StatusBarViewModel()
    {
    }
}