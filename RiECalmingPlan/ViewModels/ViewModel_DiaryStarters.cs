using MvvmHelpers;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DiaryStarters : ViewModel_Base{

        private ObservableRangeCollection<DiaryStarter> _Starters;
        private int _SelectedIndex;

        public ObservableRangeCollection<DiaryStarter> Starters { get { return _Starters; } set { SetProperty(ref _Starters, value); } }
        public int SelectedIndex { get { return _SelectedIndex; } set { SetProperty(ref _SelectedIndex, value); } }

        public ViewModel_DiaryStarters() {
            Init();
        }

        async void Init() {
            Starters = new ObservableRangeCollection<DiaryStarter>();
            Starters.AddRange(await App.database.GetDiaryStarterOptionsAsync());
        }
    }
}
