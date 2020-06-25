using MvvmHelpers;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class DistressHistoryViewModel {


        public ObservableRangeCollection<DistressResponse> FilteredHistory { get; set; } = new ObservableRangeCollection<DistressResponse>();
        public ObservableRangeCollection<DistressResponse> FullHistory { get; set; }
        public ObservableRangeCollection<string> FilterOptions { get; } = new ObservableRangeCollection<string> {"Today", "Week", "Month", "All"};

        public DistressHistoryViewModel() {
            Refresh();
        }

        public async void Refresh() {
            FullHistory = new ObservableRangeCollection<DistressResponse>();
            var calm = await App.database.GetCalmDistressResponseHistory();
            var noncalm = await App.database.GetNonCalmDistressResponseHistory();
            FullHistory.AddRange(calm);
            FullHistory.AddRange(noncalm);
        }

        void FilterItems() {
            //FilteredHistory.ReplaceRange(FullHistory.Where(a => a.TimeStamp == SelectedFilter || SelectedFilter == "All"));
        }
    }
}
