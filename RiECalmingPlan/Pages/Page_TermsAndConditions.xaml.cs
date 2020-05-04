using System;
using System.Collections.Generic;
using RiECalmingPlan.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;




namespace RiECalmingPlan.Views {
    public partial class TermsAndConditions : ContentPage {

        public TermsAndConditions() {
            InitializeComponent();
            BackgroundColor = Constants.BackgroundColor;
        }

        private async void ContinueToMenu(object sender, EventArgs e) {
            //Just use an OK semaphoer to signal the Terms and Conditions
            //  have been agreed to
            Application.Current.Properties["TandC_Accepted"] = "Agreed";
            await Application.Current.SavePropertiesAsync();
            await Navigation.PopAsync();
        }

    }
}
