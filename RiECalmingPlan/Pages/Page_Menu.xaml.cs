using System.Linq;
using RiECalmingPlan.Models;
using RiECalmingPlan.Pages;
using Xamarin.Forms;


namespace RiECalmingPlan.Views {
    public partial class Page_Menu : ContentPage {

        public Page_Menu() {
            InitializeComponent();
            BackgroundColor = Constants.BackgroundColor;

            // hello from mitchell's push
        }

        async void GoToContextMainPage(object sender, System.EventArgs e) {
            await Navigation.PushAsync(new Page_Questions());
        }

    }
}
