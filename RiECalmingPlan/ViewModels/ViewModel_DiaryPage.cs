using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DiaryPage : ViewModel_Base {


        private PageState _CurrentState;
        public PageState CurrentState { get { return _CurrentState; } set { SetProperty(ref _CurrentState, value); } }

        public enum PageState {
            COMPLETED,
            EDITING,
        }

    }
}
