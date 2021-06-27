using RiECalmingPlan.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using RiECalmingPlan.ViewModels;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_TermsAndConditions : ContentPage {
        public Page_TermsAndConditions() {
            InitializeComponent();

            /*
            // ********** FILE READING ********** 
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("RiECalmingPlan.Resource.RiETermsAndConditions.txt");
            StreamReader inputStream = new StreamReader(stream);
            string text = inputStream.ReadToEnd();
            Console.WriteLine(text);
            // ********** ********** **********

            Label_TermsAndConditions.Text = text;
            */
        }

        private async void Button_Accept_Clicked(object sender, EventArgs e) {
            if (CheckBox_Agreed.IsChecked) {
                /*
                 * This below code (Upon first entering after installed, will still show the back button once the user enters the main menu)
                 * The new code now removes this page after the new one is opened (also displays the back button for like 0.01s so it isn't great)
                 * 
                 * AppPreferences.TermsAndConditionsAccepted = true;
                 * await Navigation.PushAsync(new Page_Menu());
                 */

                AppPreferences.TermsAndConditionsBottomControls = false;
                var previousPage = Navigation.NavigationStack.LastOrDefault();//Current page (Terms and Conditions)
                await Navigation.PushAsync(new Page_Menu());
                Navigation.RemovePage(previousPage);
            } else {
                await DisplayAlert("Accept", "Please accept the terms and conditions", "OK");
            }
        }
    }
}