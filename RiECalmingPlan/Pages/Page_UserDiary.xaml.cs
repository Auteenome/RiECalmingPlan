using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiECalmingPlan.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RiECalmingPlan.Models.JSON;
using RiECalmingPlan.Views;
using MvvmHelpers;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_UserDiary : ContentPage {

        //public ViewModel_DiaryStarters starters = new ViewModel_DiaryStarters();//starters are preloaded here so they are displayed during each ADD/UPDATE entry page

        public ViewModel_UserDiary _viewModel;

        public Page_UserDiary() {
            InitializeComponent();
            BindingContext = _viewModel = new ViewModel_UserDiary();

            Init();
        }

        public async void Init() {
            if (!AppPreferences.Help_UserDiary) {
                AppPreferences.Help_UserDiary = !(await this.DisplayAlert("User Diary Tutorial", "You can add your own diary entries to this diary", "Yes", "No"));
            }
        }
        

        //I'll be honest and say this took me like half a day to find
        private void RefreshSlide_Clicked(object sender, EventArgs e) {
            //Refreshes ItemSource (The problem with this is that the state flags are jumbled. Have to fix this if xamarin forms is updated)
            //ObservableRangeCollection<ViewModel_DiaryEntry> entries = new ObservableRangeCollection<ViewModel_DiaryEntry>(_viewModel.DiaryEntries);
            //Carousel.ItemsSource = entries;


            Carousel.ClearValue(ItemsView.ItemTemplateProperty);
            Carousel.SetValue(ItemsView.ItemTemplateProperty, Resources["userDiarySelector"]);
        }
    }
}