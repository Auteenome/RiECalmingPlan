using MvvmHelpers;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class DistressHistoryViewModel : ViewModel_Base {

        private ObservableRangeCollection<DistressResponse> _FilteredHistory;
        private ObservableRangeCollection<DistressResponse> _FullHistory;
        private string _SelectedFilter = "All";

        public ObservableRangeCollection<DistressResponse> FilteredHistory { get { return _FilteredHistory; } set { SetProperty(ref _FilteredHistory, value); } }
        public ObservableRangeCollection<DistressResponse> FullHistory { get { return _FullHistory; } set { SetProperty(ref _FullHistory, value); } }
        public ObservableRangeCollection<string> FilterOptions { get; } = new ObservableRangeCollection<string> {"Today", "Week", "Month", "All"};
        public string SelectedFilter { get { return _SelectedFilter; } set { SetProperty(ref _SelectedFilter, value); FilterItems(); } }


        public DistressHistoryViewModel() {
            Refresh();
        }

        public async void Refresh() {
            FullHistory = new ObservableRangeCollection<DistressResponse>();
            FilteredHistory = new ObservableRangeCollection<DistressResponse>();
            var calm = await App.database.GetCalmDistressResponseHistory();
            var noncalm = await App.database.GetNonCalmDistressResponseHistory();
            FullHistory.AddRange(calm);
            FullHistory.AddRange(noncalm);
            FullHistory = new ObservableRangeCollection<DistressResponse>(FullHistory.OrderBy(x => x.TimeStamp).ToList());
            FilterItems();
        }

        void FilterItems() {
            Console.WriteLine("Filtering Items by " + _SelectedFilter);
            switch (SelectedFilter) {
                case "All":
                    FilteredHistory.ReplaceRange(FullHistory);
                    break;
                case "Week":
                    FilteredHistory.ReplaceRange(FullHistory.Where(a => a.TimeStamp.Date >= DateTime.Now.Date.AddDays(-7) && a.TimeStamp.Date <= DateTime.Now.Date ));
                    break;
                case "Month":
                    FilteredHistory.ReplaceRange(FullHistory.Where(a => a.TimeStamp.Date >= DateTime.Now.Date.AddMonths(-1) && a.TimeStamp.Date <= DateTime.Now.Date));
                    break;
                case "Today":
                    FilteredHistory.ReplaceRange(FullHistory.Where(a => a.TimeStamp.Date == DateTime.Now.Date));
                    break;

            }
        }
    }
}
