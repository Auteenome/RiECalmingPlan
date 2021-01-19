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

        }

        public void LevelButtonPressed(object sender, EventArgs e) {
            if (Label_DistressLevel.Text.ToString() == "Distress Level: Acute")
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Red"];
            else if (Label_DistressLevel.Text.ToString() == "Distress Level: Moderate")
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Orange"];
            else if(Label_DistressLevel.Text.ToString() == "Distress Level: Mild")
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Yellow"];
            else if(Label_DistressLevel.Text.ToString() == "Distress Level: Calm")
                Frame_SupportPlan.BackgroundColor = (Color)Application.Current.Resources["Distress_Green"];
            Carousel.Position = 1;
        }

        public void BackButtonPressed(object sender, EventArgs e) {
            Frame_SupportPlan.BackgroundColor = Color.White;
            Carousel.Position = 0;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

    }
}