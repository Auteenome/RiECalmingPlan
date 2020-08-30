using RiECalmingPlan.Models;
using System;

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

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)  // cjamge name
        {
            Label_CheckBox cbx = (Label_CheckBox)e.Item;

            Console.WriteLine(e.Item);

            //cbx.CheckBoxValue = (bool) cbx.CheckBoxValue ? 1:0;
            // await App.database.UpdateCheckBoxResponse(checkBoxLabel);
        }
    }
}