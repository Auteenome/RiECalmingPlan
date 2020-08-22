using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RiECalmingPlan.ViewModels {
    class TapOpenLinkViewModel : TapViewModel_Base {
        public override void OnTapped(object s)
        {
            if (!string.IsNullOrWhiteSpace(s.ToString()))
                Browser.OpenAsync(s.ToString(), BrowserLaunchMode.SystemPreferred);
        }
    }
}
