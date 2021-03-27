using RiECalmingPlan.Models.JSON;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DiaryCover : ViewModel_DiaryPage {

        private DiaryCover _Cover;
        public DiaryCover Cover { get { return _Cover; } set { SetProperty(ref _Cover, value); } }

        public ViewModel_DiaryCover(DiaryCover cover) {
            Cover = cover;
            CurrentState = PageState.COMPLETED;//When loading a diary entry from save file, it becomes a completed diary entry

        }

    }
}
