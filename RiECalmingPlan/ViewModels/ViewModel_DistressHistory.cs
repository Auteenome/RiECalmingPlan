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
    public class ViewModel_DistressHistory : ViewModel_Base {
        /*
         * Pulls the full history from the database, and that list can be filtered into a new list which is displayed in the View.
         * Filtering controls are also here, and a mapping that helps with defining the magnitude of a distress level is included to help with filtering.
         * 
         * 
         * 
         */

        private ObservableRangeCollection<UserInputDistressLevel> _FilteredHistory;
        private ObservableRangeCollection<UserInputDistressLevel> _FullHistory;
        private string _SelectedFilter = "All";

        public ObservableRangeCollection<UserInputDistressLevel> FilteredHistory { get { return _FilteredHistory; } set { SetProperty(ref _FilteredHistory, value); } }
        public ObservableRangeCollection<UserInputDistressLevel> FullHistory { get { return _FullHistory; } set { SetProperty(ref _FullHistory, value); } }
        public ObservableRangeCollection<string> FilterOptions { get; } = new ObservableRangeCollection<string> {"Today", "Week", "Month", "All"};
        public string SelectedFilter { get { return _SelectedFilter; } set { SetProperty(ref _SelectedFilter, value); FilterItems(); ResetChart(); } }

        /*
        private Chart _LineChart;
        public Chart LineChart { get { return _LineChart; } set { SetProperty(ref _LineChart, value); } }
        */
        private Chart _BarChart;
        public Chart BarChart { get { return _BarChart; } set { SetProperty(ref _BarChart, value); } }

        readonly Dictionary<string, int> Magnitude = new Dictionary<string, int>() {
            {"None", 0 },
            {"Calm", 1 },
            {"Mild", 2 },
            {"Moderate", 3 },
            {"Acute", 4 }
        };


        public ViewModel_DistressHistory() {
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

        /*
        private void ResetChart() {
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach (UserInputDistressLevel timestamp in FilteredHistory) {
                Magnitude.TryGetValue(timestamp.DistressLevelType, out int value);
                entries.Add(new ChartEntry(value) {
                    Label = "",
                    ValueLabel = timestamp.StartTime.ToString(),
                    Color = SKColor.Parse("#b455b6")
                });
            }
            LineChart = new LineChart() { Entries = entries };
        }
        */
        private void ResetChart() {
            string level;
            string entryDate;
            string entryTime;
            string labelEntry;
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach (UserInputDistressLevel timestamp in FilteredHistory) {
                Magnitude.TryGetValue(timestamp.DistressLevelType, out int value);
                //assign appropriate distress colour level to entry
                switch (value) {
                    case 4:
                        //Acute Red
                        level = "#ff0000";
                        break;
                    case 3:
                        //Moderate Orange
                        //level = "#f05828";
                        level = "#ffa500";
                        break;
                    case 2:
                        //Mild Yellow
                        //level = "f8d90f";
                        level = "#ffff00";
                        break;
                    case 1:
                        //calm - does not have a magniture but color is light green (Neon)
                        //level = "#8dc63f";
                        //level = "#32cd32";
                        level = "#39ff14";
                        break;
                    default:
                        level = "#b455b6";
                        break;
                }
                entryTime = timestamp.StartTime.ToString("hh:mm tt");
                entryDate = timestamp.StartTime.ToString("dd MMM"); //Gets short Date
                labelEntry = entryTime + " " + entryDate;
                entries.Add(new ChartEntry(value) {
                    //Label = "", Label appears underneath Bar Chart as the magnitude of the Bar
                    Label = value.ToString(),
                    TextColor = SKColor.Parse("#ffffff"),
                    //ValueLabel = timestamp.StartTime.ToString(),
                    //ValueLabel appears on top of the Bar as timestamp
                    ValueLabel = labelEntry,
                    ValueLabelColor = SKColor.Parse("#ffffff"),
                    //Color = SKColor.Parse("#b455b6")
                    Color = SKColor.Parse(level)
                });
            }
            //LineChart = new LineChart() { Entries = entries, MaxValue = 4, MinValue = 1, LineAreaAlpha = 1, BackgroundColor =SKColor.Parse("#FF90EE90")  };
            BarChart = new BarChart() { Entries = entries, MaxValue = 4, MinValue = 1, LabelOrientation = Orientation.Horizontal, ValueLabelOrientation = Orientation.Vertical, BackgroundColor = SKColor.Parse("#006738"), LabelColor = SKColor.Parse("#ffffff"), LabelTextSize = 25f };

        }

    }
}
