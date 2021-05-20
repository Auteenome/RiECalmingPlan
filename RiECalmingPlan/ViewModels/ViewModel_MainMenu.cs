using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_MainMenu : ViewModel_Base {


        /*
         * Last Diary Entry is the only field needed in the page since everything else is preset
         * 
         */
        private DateTime _LastDiaryEntry;

        public DateTime LastDiaryEntry { get { return _LastDiaryEntry; } set { SetProperty(ref _LastDiaryEntry, value); } }

        public ViewModel_MainMenu() {
        }

    }
}
