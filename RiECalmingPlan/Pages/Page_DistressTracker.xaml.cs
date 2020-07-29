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

        private DistressLevelViewModel ViewModel = new DistressLevelViewModel();

        public Page_DistressTracker() {
            InitializeComponent();
            Carousel.BindingContext = ViewModel;
        }

        public void LevelButtonPressed(object sender, EventArgs e) {
            Carousel.Position = 1;
        }

        public void BackButtonPressed(object sender, EventArgs e) {
            Carousel.Position = 0;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            ViewModel.StopTimer();
        }

    }
}