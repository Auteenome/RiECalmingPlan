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

        private const int PYRAMID_CONTROLS = 0;
        private const int CALM_SURVEY = 1;
        private const int NON_CALM_SURVEY = 2;


        /*
         * 
         * 0 - non calm
         * 1 - slide controls
         * 2 - calm
         */


        public Page_DistressTracker() {
            InitializeComponent();
            BindingContext = new DistressResponses();
        }

        public void NotCalmSelected(object sender, EventArgs e) {
            /* When Acute, Moderate, or Mild is selected
             * 
             * TextBox questions shown:
             * Did anything make you this way?
             * What can you do to feel better?
             * 
             * RadioBox Question shown:
             * Would you like a suggestion? Yes/No (If Yes, lead the user to a page of suggestions)
             * 
             * 
             */
            Console.WriteLine("NOT CALM SELECTED: " + ((Button)sender).Text);
            ((DistressResponses)BindingContext).NonCalmResponse.DistressLevelType = ((Button)sender).Text;
            Carousel.Position = NON_CALM_SURVEY;
        }

        private void CalmSelected(object sender, EventArgs e) {
            /* When Calm is Selected
             * 
             * TextBox questions shown:
             * Terrific – was there anything that helped you feel calm?
             * Is there anything you will do to keep you feeling this way?
             * 
             */

            Console.WriteLine("CALM SELECTED");
            Carousel.Position = CALM_SURVEY;
        }

        private async void SaveCalmResponse(object sender, EventArgs e) {
            Console.WriteLine("SAVING RESPONSE");

            var response = ((DistressResponses)BindingContext);
            response.CalmResponse.TimeStamp = DateTime.Now;


            response.CalmResponse.Response1 = CalmResponse1.Text;
            response.CalmResponse.Response2 = CalmResponse2.Text;

            Console.WriteLine("Response 1: " + response.CalmResponse.Response1 + "Response2:  " + response.CalmResponse.Response2);
            await App.database.AppendCalmResponse(response.CalmResponse);

            Back(this, EventArgs.Empty);
        }

        private async void SaveNonCalmResponse(object sender, EventArgs e) {
            Console.WriteLine("SAVING RESPONSE");

            var response = ((DistressResponses)BindingContext);
            response.NonCalmResponse.TimeStamp = DateTime.Now;

            response.NonCalmResponse.Response1 = NonCalmResponse1.Text;
            response.NonCalmResponse.Response2 = NonCalmResponse2.Text;
            response.NonCalmResponse.Suggestion = NonCalmResponse3.IsChecked;

            Console.WriteLine("Response 1: " + response.NonCalmResponse.Response1 + " Response2: " + response.NonCalmResponse.Response2 + " isChecked: " + response.NonCalmResponse.Suggestion);
            await App.database.AppendNonCalmResponse(response.NonCalmResponse);

            Back(this, EventArgs.Empty);
        }

        private void Back(object sender, EventArgs e) {
            //Clears everything and goes back to main control
            Console.WriteLine("GOING BACK");
            Carousel.Position = PYRAMID_CONTROLS;
            NonCalmResponse1.Text = "";
            NonCalmResponse2.Text = "";
            NonCalmResponse3.IsChecked = false;
            CalmResponse1.Text = "";
            CalmResponse2.Text = "";

        }
    }
}