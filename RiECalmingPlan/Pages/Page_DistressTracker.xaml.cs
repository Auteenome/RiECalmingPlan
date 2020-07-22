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

        

        public Page_DistressTracker() {
            InitializeComponent();
            Carousel.BindingContext = new DistressLevelViewModel();
        }

        public void LevelButtonPressed(object sender, EventArgs e) {
            Carousel.Position = 1;
        }

        public void BackButtonPressed(object sender, EventArgs e) {
            Carousel.Position = 0;
        }
        
    }
}