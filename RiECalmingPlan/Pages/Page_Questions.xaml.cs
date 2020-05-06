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

            /*
             * NOTE, for some weird reason, the app might close with the error
             * "Object Reference not set to an object" on question 5
             * This may be a local database issue, or threading issue, as it skipped frames doing work on main thread
             *
             * 
             * okay maybe i overreacted but it works after converting the boolean to a string
             * 
             * this is really good because we know which response to change in the database
             */
            CheckBox checkbox = (CheckBox)sender;
            int CPQID = ((Label_CheckBox)checkbox.BindingContext).CPQID;
            int CheckBoxID = ((Label_CheckBox)checkbox.BindingContext).CheckBoxID;
            string CheckText = ((Label_CheckBox)checkbox.BindingContext).CheckText;
            bool CheckBoxValue = ((Label_CheckBox)checkbox.BindingContext).CheckBoxValue;

            Console.WriteLine("\n CPQID:" + CPQID + "\n CheckBoxID: " + CheckBoxID + "\n CheckText: " + CheckText + "\n CheckBoxValue: " + CheckBoxValue.ToString());

            database.UpdateCheckBoxResponse(CPQID, CheckBoxID, CheckBoxValue);
        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e) {
            //Event that occurs when a Stepper is changed

            Stepper stepper = (Stepper)sender;
            int CPQID = ((Label_RadioBox)stepper.BindingContext).CPQID;
            int RadioBoxID = ((Label_RadioBox)stepper.BindingContext).RadioBoxID;
            string RadioBoxText = ((Label_RadioBox)stepper.BindingContext).RadioBoxText;
            int RadioBoxValue = ((Label_RadioBox)stepper.BindingContext).RadioBoxValue;

            Console.WriteLine("\n CPQID:" + CPQID + "\n RadioBoxID: " + RadioBoxID + "\n RadioBoxText: " + RadioBoxText + "\n RadioBoxValue: " + RadioBoxValue);
        }
    }
}