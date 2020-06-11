using System;
using System.Linq;
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

        void ResetLocalDatabase(object sender, EventArgs e) {
            //will be implemented soon
            App.database.ResetConnection();
            Console.WriteLine("Database connection reset");
        }


    }
}
