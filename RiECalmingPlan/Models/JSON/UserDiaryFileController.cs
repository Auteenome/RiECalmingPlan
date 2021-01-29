using Newtonsoft.Json;
using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RiECalmingPlan.Models.JSON {
    public static class UserDiaryFileController {



        public static List<ViewModel_DiaryEntry> Load() {

            if (File.Exists(App.SavePath)) {
                //Open File
                List<DiaryEntry> rawEntries = JsonConvert.DeserializeObject<List<DiaryEntry>>(File.ReadAllText(App.SavePath));
                List<ViewModel_DiaryEntry> newEntries = new List<ViewModel_DiaryEntry>();
                foreach (DiaryEntry entry in rawEntries) {
                    newEntries.Add(new ViewModel_DiaryEntry(entry));
                }
                return newEntries;
            } else {
                //Create new File
                List<ViewModel_DiaryEntry> entries = new List<ViewModel_DiaryEntry>();
                Save(null);
                return entries;
            }
        }

        public static void Save(List<ViewModel_DiaryEntry> entries) {
            //Save object as JSON File
            List<DiaryEntry> rawEntries = new List<DiaryEntry>();
            if (entries != null) {
                foreach (ViewModel_DiaryEntry entry in entries) {
                    if (entry.CurrentState != ViewModel_DiaryEntry.DiaryEntryState.NEWSPACE) {
                        rawEntries.Add(entry.Entry);
                    }
                }
            }
            File.WriteAllText(App.SavePath, JsonConvert.SerializeObject(rawEntries));


        }



    }
}
