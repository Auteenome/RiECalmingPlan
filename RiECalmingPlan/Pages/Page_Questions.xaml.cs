﻿using RiECalmingPlan.Models;
using RiECalmingPlan.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Questions : ContentPage {
        public Page_Questions() {
            InitializeComponent();

            if (!AppPreferences.Help_CalmingPlan) {
                AppPreferences.Help_CalmingPlan = true;
                this.DisplayAlert("Calming Plan Tutorial", "Please finish this survey regarding your stressors", "Okay");
            }
        }

    }
}