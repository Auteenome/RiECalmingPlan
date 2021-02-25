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
                AppPreferences.Help_UserDiary = !(await this.DisplayAlert("User Diary Tutorial", "You can add your own diary entries to this diary.\nShow Again?", "Yes", "No"));
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

        private void ToolBarHelp_Clicked(object sender, EventArgs e) {
            string title = "";
            string text = "";

            //((ViewModel_DiaryEntry)Carousel.CurrentItem).CurrentState returns null

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

        /*
         * I was dabbling with CurrentItemChanged vs PositionChanged
         * 
         * CurrentItemChanged upon initiation will always have a null PreviousItem, whereas PositionChanged will always give 0 as the PreviousPosition
         * 
         * The former will not work when attempting to transition from NEWSPACE -> EDITING on the first frame, and then swiping to save its information
         * 
         * Using PositionChanged for the above case will swipe you towards the newly created frame rather than the one that was in that position before
         * 
         * 
        private void Carousel_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) {
            //Works when going from a COMPLETED frame into EDITING frame, but not from NEWSPACE into EDITING Frame
            //Swiping an EDITING frame that was previously a NEWSPACE Frame will not save.

            Console.WriteLine("Current Item: " + ((ViewModel_DiaryEntry)e.CurrentItem).Entry.Body + " Previous Item: " + e.PreviousItem);
            if (e.PreviousItem != null && ((ViewModel_DiaryEntry)e.PreviousItem).CurrentState == ViewModel_DiaryEntry.DiaryEntryState.EDITING) {
                _viewModel.SaveEntry((ViewModel_DiaryEntry)e.PreviousItem);
            } else if (e.PreviousItem == null && ((ViewModel_DiaryEntry)e.CurrentItem).CurrentState == ViewModel_DiaryEntry.DiaryEntryState.EDITING) {
                _viewModel.SaveEntry((ViewModel_DiaryEntry)e.CurrentItem);
            }

            Carousel.ClearValue(ItemsView.ItemTemplateProperty);
            Carousel.SetValue(ItemsView.ItemTemplateProperty, Resources["userDiarySelector"]);
        }
        */
        private void Carousel_PositionChanged(object sender, PositionChangedEventArgs e) {
            //This one definitely should work
            Console.WriteLine("Previous Position: " + e.PreviousPosition + " Current Position: " + e.CurrentPosition );
            if (e.PreviousPosition < _viewModel.DiaryEntries.Count) {
                if (_viewModel.DiaryEntries[e.PreviousPosition] != null && _viewModel.DiaryEntries[e.PreviousPosition].CurrentState == ViewModel_DiaryEntry.DiaryEntryState.EDITING) {
                    _viewModel.SaveEntry(_viewModel.DiaryEntries[e.PreviousPosition]);
                }

            }

            Carousel.ClearValue(ItemsView.ItemTemplateProperty);
            Carousel.SetValue(ItemsView.ItemTemplateProperty, Resources["userDiarySelector"]);
        }
    }
}