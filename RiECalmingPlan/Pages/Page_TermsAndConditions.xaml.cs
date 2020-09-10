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

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_TermsAndConditions : ContentPage {
        public Page_TermsAndConditions()
        {
            InitializeComponent();

            // ********** FILE READING ********** 
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("RiECalmingPlan.Resource.RiETermsAndConditions.txt");
            StreamReader inputStream = new StreamReader(stream);
            string text = "";
            text = inputStream.ReadToEnd();
            Console.WriteLine(text);
            // ********** ********** **********

            Label_TermsAndConditions.Text = text;
        }

        private async void Button_Accept_Clicked(object sender, EventArgs e)
        {
            if (CheckBox_Agreed.IsChecked)
            {
                AppPreferences.TermsAndConditionsAccepted = true;
                await Navigation.PushAsync(new Page_Register());
            }
            else
            {
                await DisplayAlert("Accept", "Please accept the terms and conditions", "OK");
            }
        }
    }
}