using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    class DistressButtonDemoViewModel : TapViewModel_Base {
        public override void OnTapped(object s)
        {
            App.Current.MainPage.DisplayAlert("OnTapped", s.ToString(), "Okay");
        }
    }
}
