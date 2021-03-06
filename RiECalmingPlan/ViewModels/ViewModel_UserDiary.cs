﻿using System;
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

        private ObservableRangeCollection<ViewModel_DiaryPage> _DiaryEntries;
        public ObservableRangeCollection<ViewModel_DiaryPage> DiaryEntries { get { return _DiaryEntries; } set { SetProperty(ref _DiaryEntries, value); } }


        public Command<ViewModel_DiaryEntry> Command_SaveEntry { get; private set; }
        public Command<ViewModel_DiaryEntry> Command_EditEntry { get; private set; }
        public Command<ViewModel_DiaryEntry> Command_RemoveEntry { get; private set; }
        public Command Command_CreateEntry { get; private set; }
        public Command<ViewModel_DiaryCover> Command_EditCover { get; private set; }
        public Command<ViewModel_DiaryCover> Command_SaveCover { get; private set; }

        public event EventHandler NewDiaryEntryAdded;
        public event EventHandler EntryLimitReached;

        public ViewModel_UserDiary() {
            //Bind Commands to Functions
            Command_SaveEntry = new Command<ViewModel_DiaryEntry>(SaveEntry);
            Command_RemoveEntry = new Command<ViewModel_DiaryEntry>(RemoveEntry);
            Command_EditEntry = new Command<ViewModel_DiaryEntry>(EditEntry);
            Command_CreateEntry = new Command(CreateEntry);
            Command_SaveCover = new Command<ViewModel_DiaryCover>(SaveCover);
            Command_EditCover = new Command<ViewModel_DiaryCover>(EditCover);

            RefreshViewModel();
        }

        private void RefreshViewModel() {
            ObservableRangeCollection<ViewModel_DiaryPage> entries = new ObservableRangeCollection<ViewModel_DiaryPage>();
            entries.AddRange(UserDiaryFileController.Load());
            DiaryEntries = entries;
        }

        public void ToggleEdit(ViewModel_DiaryPage page) {
            //Toggles the state of the current page, UI will deal with the changes after this function is done

            if (page is ViewModel_DiaryCover cover) {
                if (cover.CurrentState == ViewModel_DiaryPage.PageState.COMPLETED) {
                    EditCover(cover);
                } else if (cover.CurrentState == ViewModel_DiaryPage.PageState.EDITING) {
                    SaveCover(cover);
                }
            } else if (page is ViewModel_DiaryEntry entry) {
                if (entry.CurrentState == ViewModel_DiaryPage.PageState.COMPLETED) {
                    EditEntry(entry);
                }
                else if (entry.CurrentState == ViewModel_DiaryPage.PageState.EDITING) {
                    SaveEntry(entry);
                }
            }
        }

        //Creates a new Entry in the Diary with the EDITING state
        private void CreateEntry() {
            //Creates a new entry
            if (DiaryEntries.Count <= 50) {//Diary Cover counts as diary Entry
                ViewModel_DiaryPage newEntry = new ViewModel_DiaryEntry() { Entry = new DiaryEntry() { FirstSubmit = DateTime.Now }, CurrentState = ViewModel_DiaryPage.PageState.EDITING };
                DiaryEntries.Add(newEntry);
                NewDiaryEntryAdded(this, EventArgs.Empty);
            } else {
                EntryLimitReached(this, EventArgs.Empty);
            }
        }

        //Saves the Diary Entry
        public void SaveEntry(ViewModel_DiaryEntry entry) {
            //Saves an entry
            Console.WriteLine("Saving Entry " + entry.CurrentState);

            entry.Entry.LastEdited = DateTime.Now;
            AppPreferences.LastDiaryEntry = entry.Entry.LastEdited;

            ViewModel_DiaryEntry updatedEntry = new ViewModel_DiaryEntry() { Entry = entry.Entry};
            updatedEntry.CurrentState = ViewModel_DiaryPage.PageState.COMPLETED;
            int i = DiaryEntries.IndexOf(entry);
            DiaryEntries[i] = updatedEntry;

            List<ViewModel_DiaryPage> entries = new List<ViewModel_DiaryPage>(DiaryEntries); //Adds current diary list from viewmodel
            UserDiaryFileController.Save(entries);

        }

        private async void RemoveEntry(ViewModel_DiaryEntry entry) {
            //Removes an entry
            bool answer = await App.Current.MainPage.DisplayAlert("Removing a Diary Entry", "Are you sure you want to delete this Diary Entry?", "Yes", "No");
            if (answer == true) {
                Console.WriteLine("Removing Entry ");
                DiaryEntries.Remove(entry);
                List<ViewModel_DiaryPage> entries = new List<ViewModel_DiaryPage>(DiaryEntries);
                UserDiaryFileController.Save(entries);
            }
        }

        private void EditEntry(ViewModel_DiaryEntry entry) {
            //Edits an entry
            Console.WriteLine(entry);
            //Triggered when the user clicks the EditEntry or NewEntry button
            if (entry.CurrentState == ViewModel_DiaryPage.PageState.COMPLETED) {
                //If it was a new frame, change the first submit field
                Console.WriteLine("Setting First Submit");
                (entry.Entry).FirstSubmit = DateTime.Now;
                (entry.Entry).LastEdited = DateTime.Now;
            }
            //Finally, change the current entry to be EDIT state
            //entry.CurrentState = ViewModel_DiaryEntry.DiaryEntryState.EDITING;
            ViewModel_DiaryEntry updatedEntry = new ViewModel_DiaryEntry() { Entry = entry.Entry, CurrentState = ViewModel_DiaryPage.PageState.EDITING };
            int i = DiaryEntries.IndexOf(entry);
            DiaryEntries[i] = updatedEntry;

        }

        private void EditCover(ViewModel_DiaryCover cover) {
            //Edits the Cover of the diary, which is also typically the first page of the diary
            ViewModel_DiaryCover updatedEntry = new ViewModel_DiaryCover(cover.Cover) {CurrentState = ViewModel_DiaryPage.PageState.EDITING };
            DiaryEntries[0] = updatedEntry;

            List<ViewModel_DiaryPage> entries = new List<ViewModel_DiaryPage>(DiaryEntries); //Adds current diary list from viewmodel
            UserDiaryFileController.Save(entries);
        }

        public void SaveCover(ViewModel_DiaryCover cover) {
            //Saves the Cover of the Diary
            ViewModel_DiaryCover updatedEntry = new ViewModel_DiaryCover(cover.Cover) {CurrentState = ViewModel_DiaryPage.PageState.COMPLETED };
            DiaryEntries[0] = updatedEntry;

            List<ViewModel_DiaryPage> entries = new List<ViewModel_DiaryPage>(DiaryEntries); //Adds current diary list from viewmodel
            UserDiaryFileController.Save(entries);
        }
    }
}
