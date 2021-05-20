using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models.JSON {
    public class DiaryCover : ViewModel_Base {
        /*
         * The cover only has the background, and the name belonging to the diary
         * 
         */
        private string _Name = "";
        private string _CoverBackgroundSource;

        public string Name { get { return _Name; } set { SetProperty(ref _Name, value); } }
        public string CoverBackgroundSource { get { return _CoverBackgroundSource; } set { SetProperty(ref _CoverBackgroundSource, value); } }

        public DiaryCover() {
            CoverBackgroundSource = "bg_Morning.png";
        }

    }
}
