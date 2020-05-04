using RiECalmingPlan.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Questions : ContentPage {


        private readonly Database database;


        public Page_Questions() {
            InitializeComponent();
            database = new Database();

        }

        private void RefreshCarouselView() {
            try {
                Questions.ItemsSource = database.GetDisplayQuestionList();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

        }
        protected override void OnAppearing() {
            base.OnAppearing();
            RefreshCarouselView();
        }


        
        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e) {
            //Event that occurs when a checkbox is checked
            Console.WriteLine("Checkbox changed " + e.Value);

        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e) {
            //Event that occurs when a Stepper is changed
            Console.WriteLine("Stepper changed " + e.NewValue);
        }
    }
}