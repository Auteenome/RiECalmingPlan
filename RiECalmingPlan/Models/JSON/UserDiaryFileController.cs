using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RiECalmingPlan.Models.JSON {
    public static class UserDiaryFileController {



        public static List<DiaryEntry> Load() {

            if (File.Exists(App.SavePath)) {
                //Open File
                return JsonConvert.DeserializeObject<List<DiaryEntry>>(File.ReadAllText(App.SavePath));
            } else {
                //Create new File
                List<DiaryEntry> entries = new List<DiaryEntry>();
                Save(entries);
                return entries;
            }
        }

        public static void Save(List<DiaryEntry> entries) {
            //Save object as JSON File
            File.WriteAllText(App.SavePath, JsonConvert.SerializeObject(entries));
        }
    }
}
