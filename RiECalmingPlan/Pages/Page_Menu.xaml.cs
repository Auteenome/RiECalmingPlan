using System;
using System.Linq;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using RiECalmingPlan.Models;
using RiECalmingPlan.Pages;
using Xamarin.Forms;


namespace RiECalmingPlan.Pages {
    public partial class Page_Menu : ContentPage {

        
        public Page_Menu() {
            InitializeComponent();
        }

        async void GoToContextMainPage(object sender, EventArgs e) {
            await Navigation.PushAsync(new Page_Questions());
        }

        async void GoToDistressTracker(object sender, EventArgs e) {
            await Navigation.PushAsync(new Page_DistressTracker());
        }

        async void GoToDistressHistory(object sender, EventArgs e) {
            await Navigation.PushAsync(new Page_DistressHistory());
        }

        async void GoToUserDiary(object sender, EventArgs e) {
            //Fingerprint stuff https://github.com/smstuebe/xamarin-fingerprint
            var request = new AuthenticationRequestConfiguration("Fingerprint Protected", "Use your finger to unlock your diary!") {
                AllowAlternativeAuthentication = true
            };
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            //If we need to, we can also request an alternative method for logging in, it will replace the 'cancel' button in the dialog box
            if (result.Authenticated) {
                await Navigation.PushAsync(new Page_UserDiary());
            }
            //Too many attempts will also lock them out for a while, but not sure how long exactly.
            //If the alternative method is not added and fingerprint is not already put into the device, it will not allow the user to enter into the diary at all
        }

        async void GoToDeviceInfo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page_DeviceInfo());
        }

        void ResetLocalDatabase(object sender, EventArgs e) {
            //will be implemented soon
            App.database.ResetConnection();
            Console.WriteLine("Database connection reset");
        }

        async private void TbItemAbout_Clicked(object sender, EventArgs e)
        {

        }
    }
}
