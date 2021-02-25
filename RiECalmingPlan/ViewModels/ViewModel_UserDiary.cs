using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MvvmHelpers;
using MvvmHelpers.Commands;
using RiECalmingPlan.Models;
using RiECalmingPlan.Models.JSON;
using RiECalmingPlan.Views;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_UserDiary : ViewModel_Base {
        /*
         * Holds a list of diary entries.
         * This view model is used for the following features:
         * 1. viewing entry metadata
         * 2. allowing an entry to be opened
         * 3. Deletion of an entry (Hidden until the user swipes right on an item)
         * 
         * 
         */

        private ObservableRangeCollection<DiaryStarter> _DiaryStarters;
        public ObservableRangeCollection<DiaryStarter> DiaryStarters { get { return _DiaryStarters; } set { SetProperty(ref _DiaryStarters, value); } }

        private string _SelectedStarter;
        public string SelectedStarter { get { return _SelectedStarter; } set { SetProperty(ref _SelectedStarter, value); } }

        private ObservableRangeCollection<ViewModel_DiaryEntry> _DiaryEntries;
        public ObservableRangeCollection<ViewModel_DiaryEntry> DiaryEntries { get {return _DiaryEntries; } set { SetProperty(ref _DiaryEntries, value); } }

        private int _Index = 0;
        public int Index { get { return _Index; } set { SetProperty(ref _Index, value); } }


        public Command<ViewModel_DiaryEntry> Command_SaveEntry { get; private set; }
        public Command<ViewModel_DiaryEntry> Command_EditEntry { get; private set; }
        public Command<ViewModel_DiaryEntry> Command_RemoveEntry { get; private set; }

        public ViewModel_UserDiary() {
            //Bind Commands to Functions
            Command_SaveEntry = new Command<ViewModel_DiaryEntry>(SaveEntry);
            Command_RemoveEntry = new Command<ViewModel_DiaryEntry>(RemoveEntry);
            Command_EditEntry = new Command<ViewModel_DiaryEntry>(EditEntry);

            RefreshViewModel();
        }

        private async void RefreshViewModel() {

            ObservableRangeCollection<ViewModel_DiaryEntry> entries = new ObservableRangeCollection<ViewModel_DiaryEntry>() {
                new ViewModel_DiaryEntry() { Entry = new DiaryEntry() }
            };
            entries.AddRange(UserDiaryFileController.Load());


            DiaryStarters = await App.database.GetDiaryStarterOptionsAsync();

            DiaryEntries = entries;
        }

        public void SaveEntry(ViewModel_DiaryEntry entry) {
            Console.WriteLine("Saving Entry ");

            entry.Entry.LastEdited = DateTime.Now;
            AppPreferences.LastDiaryEntry = entry.Entry.LastEdited;

            List<ViewModel_DiaryEntry> entries = new List<ViewModel_DiaryEntry>();
            entries.AddRange(DiaryEntries);//Adds current diary list from viewmodel
            UserDiaryFileController.Save(entries);


            RefreshViewModel();
        }

        private async void RemoveEntry(ViewModel_DiaryEntry entry) {
            Console.WriteLine("Removing Entry ");
            bool answer = await App.Current.MainPage.DisplayAlert("Removing a Diary Entry", "Are you sure you want to delete this Diary Entry?", "Yes", "No");
            if (answer == true) {
                List<ViewModel_DiaryEntry> entries = new List<ViewModel_DiaryEntry>(DiaryEntries);
                entries.Remove(entry);
                UserDiaryFileController.Save(entries);

                RefreshViewModel();
            }
        }

        private void EditEntry(ViewModel_DiaryEntry entry) {
            Console.WriteLine(entry);
            //Triggered when the user clicks the EditEntry or NewEntry button
            if (entry.CurrentState == ViewModel_DiaryEntry.DiaryEntryState.NEWSPACE) {
                //If it was a new frame, change the first submit field
                Console.WriteLine("Setting First Submit");
                entry.Entry.FirstSubmit = DateTime.Now;
                entry.Entry.LastEdited = DateTime.Now;
            }
            Console.WriteLine("Editing Entry ");
            foreach (ViewModel_DiaryEntry e in DiaryEntries) {
                //Flip all Editing states in each entry to be completed
                if (e.CurrentState == ViewModel_DiaryEntry.DiaryEntryState.EDITING) {
                    e.CurrentState = ViewModel_DiaryEntry.DiaryEntryState.COMPLETED;
                }
            }
            //Finally, change the current entry to be EDIT state
            entry.CurrentState = ViewModel_DiaryEntry.DiaryEntryState.EDITING;
        }
    }
}
