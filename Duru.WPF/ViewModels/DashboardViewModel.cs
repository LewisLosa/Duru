// DashboardViewModel.cs
using Duru.Core.Business;
using Duru.Core.Services;
using Duru.Core.ViewModels;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Duru.WPF.ViewModels
{
    public class DashboardViewModel : ReactiveObject
    {
        private readonly OccupancyCalculator _occupancyCalculator;
        private Duru.Core.ViewModels.DashboardViewModel _stats;
        private bool _isLoading;

        public Duru.Core.ViewModels.DashboardViewModel Stats
        {
            get => _stats;
            set => this.RaiseAndSetIfChanged(ref _stats, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        public ObservableCollection<ReservationViewModel> RecentReservations { get; } = new();

        public DashboardViewModel(OccupancyCalculator occupancyCalculator)
        {
            _occupancyCalculator = occupancyCalculator;

            // Load data when the view model is created
            LoadDataAsync().ConfigureAwait(false);

            // Refresh data every 5 minutes
            Observable.Interval(TimeSpan.FromMinutes(5))
                .Subscribe(async _ => await LoadDataAsync().ConfigureAwait(false));
        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;

            try
            {
                var stats = await _occupancyCalculator.GetOccupancyStatsAsync();
                Stats = stats;

                if (stats.RecentReservations != null)
                {
                    RecentReservations.Clear();
                    foreach (var reservation in stats.RecentReservations)
                    {
                        RecentReservations.Add(reservation);
                    }
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
