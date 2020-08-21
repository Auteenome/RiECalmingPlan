using System;
using System.Linq;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using RiECalmingPlan.Models;
using RiECalmingPlan.Pages;
using Xamarin.Forms;


namespace RiECalmingPlan.Views {
    public partial class Page_Menu : ContentPage {

        
        public Page_Menu() {
            InitializeComponent();
            // BackgroundColor = Constants.BackgroundColor;
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
            var request = new AuthenticationRequestConfiguration("Fingerprint Protected", "Use your finger to unlock your diary!");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if (result.Authenticated) {
                await Navigation.PushAsync(new Page_UserDiary());
            }
        }

        async void GoToButtonDemo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page_ButtonDemo());
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


    }
}
