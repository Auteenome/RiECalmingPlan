using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RiECalmingPlan.ViewModels {
    class TapPhoneDialerViewModel : TapViewModel_Base {
        public override void OnTapped(object s)
        {
            if (s.ToString() != null)
                PhoneDialer.Open(s.ToString());
        }
    }
}
