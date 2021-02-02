using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RiECalmingPlan.ViewModels {
    public class AppPreferences : ViewModel_Base {
        // Xamarin.Essentials Preferences API: https://docs.microsoft.com/en-us/xamarin/essentials/preferences?context=xamarin%2Fxamarin-forms&tabs=android
        public static bool AccountCreated {
            get => Preferences.Get(nameof(AccountCreated), false);
            set => Preferences.Set(nameof(AccountCreated), value);
        }

        /*
        public static string UserEmailAddress {
            get => Preferences.Get(nameof(UserEmailAddress), null);
            set => Preferences.Set(nameof(UserEmailAddress), value);
        }

        public static string UserPassword {
            get => Preferences.Get(nameof(UserPassword), null);
            set => Preferences.Set(nameof(UserPassword), value);
        }
        */

        public static bool TermsAndConditionsBottomControls {
            get => Preferences.Get(nameof(TermsAndConditionsBottomControls), true);
            set => Preferences.Set(nameof(TermsAndConditionsBottomControls), value);
        }

        public static bool FirstTimeOpened {
            get => Preferences.Get(nameof(FirstTimeOpened), true);
            set => Preferences.Set(nameof(FirstTimeOpened), value);
        }

        public static DateTime LastDiaryEntry {
            get => Preferences.Get(nameof(LastDiaryEntry), DateTime.MinValue);
            set => Preferences.Set(nameof(LastDiaryEntry), value);
        }
    }
}
