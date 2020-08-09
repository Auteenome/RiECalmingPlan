using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MvvmHelpers.Commands;
using RiECalmingPlan.Models.JSON;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DiaryEntries : ViewModel_Base {
        /*
         * Holds a list of diary entries.
         * This view model is used for the following features:
         * 1. viewing entry metadata
         * 2. allowing an entry to be opened
         * 
         */
        private List<DiaryEntry> _Entries;
        public List<DiaryEntry> Entries { get { return _Entries; } set { SetProperty(ref _Entries, value); } }

        public ViewModel_DiaryEntries() {
            Refresh();
        }

        public void Refresh() {
            Entries = UserDiaryFileController.Load();
        }

        public void Reset() {
            Entries = new List<DiaryEntry>();
            UserDiaryFileController.Save(Entries);
        }

        public void AddEntry(object sender, EventArgs e) {
            ((DiaryEntry)sender).FirstSubmit = DateTime.Now;
            ((DiaryEntry)sender).LastEdited = DateTime.Now;
            Entries.Add(((DiaryEntry)sender));
            UserDiaryFileController.Save(Entries);
        }

        public void UpdateEntry(object sender, EventArgs e) {
            ((DiaryEntry)sender).LastEdited = DateTime.Now;
            Entries.Add(((DiaryEntry)sender));
            UserDiaryFileController.Save(Entries);
        }
    }
}
