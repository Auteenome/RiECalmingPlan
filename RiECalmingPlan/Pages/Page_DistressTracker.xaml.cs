using RiECalmingPlan.Models;
using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_DistressTracker : ContentPage {

        private readonly ViewModel_DistressLevel ViewModel = new ViewModel_DistressLevel();

        

        public Page_DistressTracker() {
            InitializeComponent();
            Carousel.BindingContext = ViewModel;

            if (!AppPreferences.Help_DistressTracker) {
                AppPreferences.Help_DistressTracker = true;
                this.DisplayAlert("Distress Tracker Tutorial", "You can find your expressions and interventions here", "Okay");
            }

        }

        public void LevelButtonPressed(object sender, EventArgs e) {
            //Definitely have to simplify this somewhere else
            if (Label_DistressLevel.Text.ToString() == "Distress Level: Acute") {
                Frame_Suggestions.BackgroundColor = (Color)Application.Current.Resources["Distress_Red"];
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Red"];
            } else if (Label_DistressLevel.Text.ToString() == "Distress Level: Moderate") {
                Frame_Suggestions.BackgroundColor = (Color)Application.Current.Resources["Distress_Orange"];
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Orange"];
            } else if (Label_DistressLevel.Text.ToString() == "Distress Level: Mild") {
                Frame_Suggestions.BackgroundColor = (Color)Application.Current.Resources["Distress_Yellow"];
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Yellow"];
            } else if (Label_DistressLevel.Text.ToString() == "Distress Level: Calm") {
                Frame_Suggestions.BackgroundColor = (Color)Application.Current.Resources["Distress_Green"];
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Green"];
            }
            Carousel.Position = 1;
        }

        public void YesButtonPressed(object sender, EventArgs e) {
            Carousel.Position = 2;
        }

        public void NoButtonPressed(object sender, EventArgs e) {
            Carousel.Position = 0;
        }

        public async void ContinueButtonPressed(object sender, EventArgs e) {
            await Application.Current.MainPage.DisplayAlert("Rest In Essence App", "Your Mood has been Saved", "Okay");
            await Navigation.PopAsync();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

    }
}