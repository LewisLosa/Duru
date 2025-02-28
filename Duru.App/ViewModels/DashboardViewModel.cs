using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Duru.Core.Business;
using Duru.Core.Services;
using Duru.Core.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Duru.App.ViewModels
{
    public partial class DashboardViewModel : ViewModelBase
    {
        private readonly OccupancyCalculator _occupancyCalculator;
        private readonly IReservationService _reservationService;

        [ObservableProperty]
        private int _totalRooms;

        [ObservableProperty]
        private int _availableRooms;

        [ObservableProperty]
        private int _occupiedRooms;

        [ObservableProperty]
        private int _maintenanceRooms;

        [ObservableProperty]
        private int _activeReservations;

        [ObservableProperty]
        private int _todayCheckIns;

        [ObservableProperty]
        private int _todayCheckOuts;

        [ObservableProperty]
        private double _todayRevenue;

        [ObservableProperty]
        private ObservableCollection<ReservationViewModel> _recentReservations = new();

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Today;

        public DashboardViewModel(OccupancyCalculator occupancyCalculator, IReservationService reservationService)
        {
            Title = "Dashboard";
            _occupancyCalculator = occupancyCalculator;
            _reservationService = reservationService;
        }

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var stats = await _occupancyCalculator.GetOccupancyStatsAsync();

                // Update properties with retrieved data
                TotalRooms = stats.TotalRooms;
                AvailableRooms = stats.AvailableRooms;
                OccupiedRooms = stats.OccupiedRooms;
                MaintenanceRooms = stats.MaintenanceRooms;
                ActiveReservations = stats.ActiveReservations;
                TodayCheckIns = stats.TodayCheckIns;
                TodayCheckOuts = stats.TodayCheckOuts;
                TodayRevenue = stats.TodayRevenue;

                // Load recent reservations
                if (stats.RecentReservations != null)
                {
                    RecentReservations.Clear();
                    foreach (var reservation in stats.RecentReservations)
                    {
                        RecentReservations.Add(reservation);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load dashboard data: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            await LoadDataAsync();
        }

        [RelayCommand]
        private async Task DateChangedAsync()
        {
            await LoadDataAsync();
        }
    }
}