using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_MainMenu : ViewModel_Base {

        private DateTime _LastDiaryEntry;

        public DateTime LastDiaryEntry { get { return _LastDiaryEntry; } set { SetProperty(ref _LastDiaryEntry, value); } }

        public ViewModel_MainMenu() {
        }

    }
}
