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

            //Init();
        }

        /*
        public async void Init() {
            
            if (!AppPreferences.Help_DistressHistory) {
                AppPreferences.Help_DistressHistory = !(await this.DisplayAlert("Distress Tracker Tutorial", "You can view all the times you have interacted with the pyramid.\nShow Again?", "Yes", "No"));
            }
            

        }
        */
        protected override async void OnAppearing() {
            base.OnAppearing();
            if (!AppPreferences.Help_DistressHistory) {
                AppPreferences.Help_DistressHistory = true;
                await Navigation.PushAsync(new Page_Help() { BindingContext = new ViewModel_Help("DistressHistoryPage") });
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new Page_Help() { BindingContext = new ViewModel_Help("DistressHistoryPage") });
        }
    }
}