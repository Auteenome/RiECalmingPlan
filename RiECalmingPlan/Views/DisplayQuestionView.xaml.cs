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


        public DisplayQuestionView() {
            InitializeComponent();
            RefreshCarouselView();
        }

        private async void RefreshCarouselView() {
            try {
                Questions.ItemsSource = await App.database.GetDisplayQuestionList();
            } catch (Exception e) {
                Console.WriteLine(e);
            }

        }

        private async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e) {
            CheckBox checkbox = (CheckBox)sender;
            Label_CheckBox checkBoxLabel = ((Label_CheckBox)checkbox.BindingContext);

           await App.database.UpdateCheckBoxResponse(checkBoxLabel);
        }

        private async void OnStepperValueChanged(object sender, ValueChangedEventArgs e) {
            //Event that occurs when a Stepper is changed

            Stepper stepper = (Stepper)sender;
            Label_Stepper stepperLabel = ((Label_Stepper)stepper.BindingContext);
            
           await App.database.UpdateStepperResponse(stepperLabel);
        }

        private async void Editor_Completed(object sender, EventArgs e) {
            Editor editor = (Editor)sender;
            Label_TextResponse editorLabel = ((Label_TextResponse)editor.BindingContext);
            await App.database.UpdateTextResponse(editorLabel);
            Console.WriteLine("Editing completed");
        }
    }
}