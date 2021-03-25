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

        public ViewModel_UserDiary _viewModel;

        public Page_UserDiary() {
            InitializeComponent();
            BindingContext = _viewModel = new ViewModel_UserDiary();

            Init();
        }

        public async void Init() {
            if (!AppPreferences.Help_UserDiary) {
                AppPreferences.Help_UserDiary = !(await this.DisplayAlert("User Diary Tutorial", "You can add your own diary entries to this diary.\nShow Again?", "Yes", "No"));
            }
        }

        private void ToolBarHelp_Clicked(object sender, EventArgs e) {
            string title = "";
            string text = "";

            switch (_viewModel.DiaryEntries[Carousel.Position].CurrentState) {
                case ViewModel_DiaryEntry.DiaryEntryState.NEWSPACE:
                    title = "New Diary Entry Space";
                    text = "This is your diary, you can add a new entry by clicking the button down below.";
                    break;
                case ViewModel_DiaryEntry.DiaryEntryState.EDITING:
                    title = "Editing Diary Entry Space";
                    text = "Please fill out the diary entry and click \"Save Diary Entry\" when you're done";
                    break;
                case ViewModel_DiaryEntry.DiaryEntryState.COMPLETED:
                    title = "Completed Diary Entry Space";
                    text = "You can read, edit or delete your completed diary entry";
                    break;
                default:
                    break;

            }
            DisplayAlert(title, text, "Okay");
        }

        private void Carousel_PositionChanged(object sender, PositionChangedEventArgs e) {
            //When the user swipes, if the user was currently editing a diary entry, it will save that diary entry.
            Console.WriteLine("Previous Position: " + e.PreviousPosition + " Current Position: " + e.CurrentPosition );
            if (e.PreviousPosition < _viewModel.DiaryEntries.Count) {
                if (_viewModel.DiaryEntries[e.PreviousPosition] != null && _viewModel.DiaryEntries[e.PreviousPosition].CurrentState == ViewModel_DiaryEntry.DiaryEntryState.EDITING) {
                    _viewModel.SaveEntry(_viewModel.DiaryEntries[e.PreviousPosition]);
                }

            }
        }

        private void ToolbarPlus_Clicked(object sender, EventArgs e) {
            //This function scrolls the carousel all the way to the end. When a new item is added to the end of the Itemsource.
            //This automatically triggers the Carousel_PositionChanged function as it goes towards the end, saving all subsequent entries. 
            Carousel.ScrollTo(_viewModel.DiaryEntries.Count-1);
        }
    }
}