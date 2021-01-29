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

        private ObservableRangeCollection<ViewModel_DiaryEntry> _DiaryEntries;
        public ObservableRangeCollection<ViewModel_DiaryEntry> DiaryEntries { get {return _DiaryEntries; } set { SetProperty(ref _DiaryEntries, value); } }

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

        private void RefreshViewModel() {
            ObservableRangeCollection<ViewModel_DiaryEntry> entries = new ObservableRangeCollection<ViewModel_DiaryEntry>();
            entries.AddRange(UserDiaryFileController.Load());
            entries.Add(new ViewModel_DiaryEntry() { Entry = new DiaryEntry() });

            DiaryEntries = entries;
        }

        private void SaveEntry(ViewModel_DiaryEntry entry) {
            Console.WriteLine("Saving Entry ");

            List<ViewModel_DiaryEntry> entries = new List<ViewModel_DiaryEntry>();
            entries.AddRange(DiaryEntries);//Adds current diary list from viewmodel
            UserDiaryFileController.Save(entries);

            RefreshViewModel();
        }

        private void RemoveEntry(ViewModel_DiaryEntry entry) {
            Console.WriteLine("Removing Entry ");

            DiaryEntries.Remove(entry);
            List<ViewModel_DiaryEntry> entries = new List<ViewModel_DiaryEntry>();
            entries.AddRange(DiaryEntries);
            UserDiaryFileController.Save(entries);

            RefreshViewModel();
        }

        private void EditEntry(ViewModel_DiaryEntry entry) {
            Console.WriteLine("Editing Entry ");

            entry.CurrentState = ViewModel_DiaryEntry.DiaryEntryState.EDITING;
        }
    }
}
