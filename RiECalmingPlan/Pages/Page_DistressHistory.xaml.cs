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
    public partial class Page_DistressHistory : ContentPage {
        public Page_DistressHistory() {
            InitializeComponent();
            BindingContext = new ViewModel_DistressHistory();

            if (!AppPreferences.Help_DistressHistory) {
                AppPreferences.Help_DistressHistory = true;
                this.DisplayAlert("Distress Tracker Tutorial", "You can view all the times you have interacted with the pyramid", "Okay");
            }
        }
    }
}