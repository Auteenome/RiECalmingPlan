﻿using RiECalmingPlan.Models;
using RiECalmingPlan.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayQuestionView : ContentView {

        ViewModel_DisplayQuestionView _viewModel;

        public DisplayQuestionView() {
            InitializeComponent();
            BindingContext = _viewModel = new ViewModel_DisplayQuestionView();
        }

        private async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e) {
            CheckBox checkbox = (CheckBox)sender;
            Label_CheckBox checkBoxLabel = ((Label_CheckBox)checkbox.BindingContext);

            await App.database.UpdateCheckBoxResponse(checkBoxLabel);
        }

        private void CheckBoxItemTapped(object sender, ItemTappedEventArgs e) {
            Label_CheckBox checkBoxLabel = (Label_CheckBox)e.Item;

            checkBoxLabel.Value = checkBoxLabel.Value == 0 ? 1:0;
        }

        private void LastSlideButtonClicked(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        private void CustomEntry_Completed(object sender, EventArgs e) {
            _viewModel.Questions[Questions.Position].AddResponse((sender as Entry).Text);
        }
    }
}