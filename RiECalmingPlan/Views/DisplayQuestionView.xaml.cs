using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayQuestionView : ContentView {

        private readonly Database database;

        public DisplayQuestionView() {
            database = new Database();
            InitializeComponent();
            RefreshCarouselView();
        }

        private async void RefreshCarouselView() {
            try {
                Questions.ItemsSource = await database.GetDisplayQuestionList();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

        }

        private async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e) {
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
             * 
             * i didn't overreact it still crashes when moving from card to card
             * when there is ticked boxes in previous fields or when moving through cards fast
             * 
             * 
             */
            CheckBox checkbox = (CheckBox)sender;
            Label_CheckBox checkBoxLabel = ((Label_CheckBox)checkbox.BindingContext);

            //Console.WriteLine("\n CPQID:" + checkBoxLabel.CPQID + "\n CheckBoxID: " + checkBoxLabel.CheckBoxID + "\n CheckText: " + checkBoxLabel.CheckText + "\n CheckBoxValue: " + checkBoxLabel.CheckBoxValue);

           await database.UpdateCheckBoxResponse(checkBoxLabel);
        }

        private async void OnStepperValueChanged(object sender, ValueChangedEventArgs e) {
            //Event that occurs when a Stepper is changed

            Stepper stepper = (Stepper)sender;
            Label_Stepper stepperLabel = ((Label_Stepper)stepper.BindingContext);

            //Console.WriteLine("\n CPQID:" + stepperLabel.CPQID + "\n RadioBoxID: " + stepperLabel.StepperID + "\n RadioBoxText: " + stepperLabel.StepperText + "\n RadioBoxValue: " + stepperLabel.StepperValue);
            
           await database.UpdateStepperResponse(stepperLabel);
        }

    }
}