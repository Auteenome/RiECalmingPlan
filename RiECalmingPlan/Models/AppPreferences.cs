using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RiECalmingPlan.Models {
    public static class AppPreferences {
        // Xamarin.Essentials Preferences API: https://docs.microsoft.com/en-us/xamarin/essentials/preferences?context=xamarin%2Fxamarin-forms&tabs=android
        public static bool AccountCreated {
            get => Preferences.Get(nameof(AccountCreated), false);
            set => Preferences.Set(nameof(AccountCreated), value);
        }

        public static bool TermsAndConditionsAccepted {
            get => Preferences.Get(nameof(TermsAndConditionsAccepted), false);
            set => Preferences.Set(nameof(TermsAndConditionsAccepted), value);
        }
    }
}
