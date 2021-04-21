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
            _viewModel.NewDiaryEntryAdded += EntryAdded;

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

            if (_viewModel.DiaryEntries[Carousel.Position] is ViewModel_DiaryCover cover) {
                title = "Diary Cover";
                if (cover.CurrentState == ViewModel_DiaryPage.PageState.EDITING) {
                    text = "You can put your name on the Diary and change the cover background";
                } else {
                    text = "This is your diary cover";
                }
            } else if (_viewModel.DiaryEntries[Carousel.Position] is ViewModel_DiaryEntry entry) {
                title = "Diary Entry";
                if (entry.CurrentState == ViewModel_DiaryPage.PageState.EDITING) {
                    text = "You are now editing a diary entry, when you are done, click the save button or swipe to a new slide";
                } else {
                    text = "This is one of your diary entries, you may edit or delete these";
                }
            }
            DisplayAlert(title, text, "Okay");
        }

        private void Carousel_PositionChanged(object sender, PositionChangedEventArgs e) {
            //When the user swipes, if the user was currently editing a diary entry, it will save that diary entry.
            Console.WriteLine("[PositionChanged] Number of Pages: " + (_viewModel.DiaryEntries.Count -1));
            Console.WriteLine("[PositionChanged] Previous Position: " + e.PreviousPosition + " Current Position: " + e.CurrentPosition );
            if (e.PreviousPosition < _viewModel.DiaryEntries.Count) {
                if (_viewModel.DiaryEntries[e.PreviousPosition] != null && _viewModel.DiaryEntries[e.PreviousPosition].CurrentState == ViewModel_DiaryPage.PageState.EDITING){
                    if (_viewModel.DiaryEntries[e.PreviousPosition] is ViewModel_DiaryCover cover) {
                        _viewModel.SaveCover(cover);
                    } else if (_viewModel.DiaryEntries[e.PreviousPosition] is ViewModel_DiaryEntry entry){
                        _viewModel.SaveEntry(entry);
                    }


                }
            }

            
            UpdateEditSaveToolbarText();
        }

        private async void EntryAdded(object sender, EventArgs e) {
            //This function scrolls the carousel all the way to the end. When a new item is added to the end of the Itemsource.
            //This automatically triggers the Carousel_PositionChanged function as it goes towards the end, saving all subsequent entries.

            //In Xamarin Forms 5.0, there is an IOS bug where the ScrollTo function would always put the current item to the first entry,
            //The fix for this was to set the animate variable to false, so that on IOS it will still take the user to the last entry
            await Task.Delay(500); //Necessary to get the last page displayed as an Editing diary page
            Console.WriteLine("[EntryAdded] Number of Pages " + _viewModel.DiaryEntries.Count);
            if (Device.RuntimePlatform == Device.iOS) {

                int num = _viewModel.DiaryEntries.Count - 1;
                Console.WriteLine("Scrolling to position " + num);
                Carousel.ScrollTo(num, animate: false);

            }else if (Device.RuntimePlatform == Device.Android) {
                Carousel.ScrollTo(index: _viewModel.DiaryEntries.Count - 1);
            }
        }

        private void ToolbarEditSave_Clicked(object sender, EventArgs e) {
            _viewModel.ToggleEdit(_viewModel.DiaryEntries[Carousel.Position]);
            UpdateEditSaveToolbarText();
        }

        private async void UpdateEditSaveToolbarText() {
            await Task.Delay(500);
            //The Edit/Save button toggles between the states. So this function will update the toolbar item to accompany that
            if (_viewModel.DiaryEntries[Carousel.Position].CurrentState == ViewModel_DiaryPage.PageState.COMPLETED) {
                EditSaveToolbarButton.IconImageSource = "baseline_edit_24.png";
                //EditSaveToolbarButton.Text = "Edit";
            } else if (_viewModel.DiaryEntries[Carousel.Position].CurrentState == ViewModel_DiaryPage.PageState.EDITING) {
                EditSaveToolbarButton.IconImageSource = "baseline_save_24.png";
                //EditSaveToolbarButton.Text = "Save";
            }
        }
    }
}

    }
}