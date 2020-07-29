using Microcharts;
using MvvmHelpers;
using RiECalmingPlan.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class DistressHistoryViewModel : ViewModel_Base {

        private ObservableRangeCollection<UserInputDistressLevel> _FilteredHistory;
        private ObservableRangeCollection<UserInputDistressLevel> _FullHistory;
        private string _SelectedFilter = "All";

        public ObservableRangeCollection<UserInputDistressLevel> FilteredHistory { get { return _FilteredHistory; } set { SetProperty(ref _FilteredHistory, value); } }
        public ObservableRangeCollection<UserInputDistressLevel> FullHistory { get { return _FullHistory; } set { SetProperty(ref _FullHistory, value); } }
        public ObservableRangeCollection<string> FilterOptions { get; } = new ObservableRangeCollection<string> {"Today", "Week", "Month", "All"};
        public string SelectedFilter { get { return _SelectedFilter; } set { SetProperty(ref _SelectedFilter, value); FilterItems(); ResetChart(); } }

        private Chart _LineChart;
        public Chart LineChart { get { return _LineChart; } set { SetProperty(ref _LineChart, value); } }

        readonly Dictionary<string, int> Magnitude = new Dictionary<string, int>() {
            {"None", 0 },
            {"Calm", 1 },
            {"Mild", 2 },
            {"Moderate", 3 },
            {"Acute", 4 }
        };


        public DistressHistoryViewModel() {
            Refresh();
        }

        public async void Refresh() {
            FullHistory = new ObservableRangeCollection<UserInputDistressLevel>();
            FilteredHistory = new ObservableRangeCollection<UserInputDistressLevel>();
            var list = await App.database.GetUserInputDistressLevels();
            FullHistory.AddRange(list);
            FullHistory = new ObservableRangeCollection<UserInputDistressLevel>(FullHistory.OrderBy(x => x.StartTime).ToList());
            FilterItems();
            ResetChart();

        }

        void FilterItems() {
            Console.WriteLine("Filtering Items by " + _SelectedFilter);
            switch (SelectedFilter) {
                case "All":
                    FilteredHistory.ReplaceRange(FullHistory);
                    break;
                case "Week":
                    FilteredHistory.ReplaceRange(FullHistory.Where(a => a.StartTime.Date >= DateTime.Now.Date.AddDays(-7) && a.StartTime.Date <= DateTime.Now.Date));
                    break;
                case "Month":
                    FilteredHistory.ReplaceRange(FullHistory.Where(a => a.StartTime.Date >= DateTime.Now.Date.AddMonths(-1) && a.StartTime.Date <= DateTime.Now.Date));
                    break;
                case "Today":
                    FilteredHistory.ReplaceRange(FullHistory.Where(a => a.StartTime.Date == DateTime.Now.Date));
                    break;

            }
        }

        private void ResetChart() {
            List<Entry> entries = new List<Entry>(); 
            foreach (UserInputDistressLevel timestamp in FilteredHistory) {
                Magnitude.TryGetValue(timestamp.DistressLevelType, out int value);
                entries.Add(new Entry(value) {
                    ValueLabel = timestamp.StartTime.ToString(),
                    Color = SKColor.Parse("#b455b6")
                });
            }
            LineChart = new LineChart() { Entries = entries };
        }
    }
}
