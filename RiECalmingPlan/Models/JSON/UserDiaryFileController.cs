using Newtonsoft.Json;
using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RiECalmingPlan.Models.JSON {
    public static class UserDiaryFileController {

        //The settings parameter allows for individual subclasses to be saved when being serialised/deserialised
        //This allows for a list of objects that have the same parent to be saved as the type of the child.
        static JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public static List<ViewModel_DiaryPage> Load() {

            if (File.Exists(App.SavePath)) {
                //Open File
                FileInfo fileInfo = new FileInfo(App.SavePath);
                Console.WriteLine("JSON File Loaded, Size of file " + fileInfo.Length);
                List<ViewModel_DiaryPage> pages = JsonConvert.DeserializeObject<List<ViewModel_DiaryPage>>(File.ReadAllText(App.SavePath), settings);

                //When loading up, everything should already be in completed state.
                foreach (ViewModel_DiaryPage page in pages) {
                    page.CurrentState = ViewModel_DiaryPage.PageState.COMPLETED;
                }

                return pages;
            } else {
                //Create new File
                Console.WriteLine("JSON file does not exist. Creating new JSON File");
                List<ViewModel_DiaryPage> pages = new List<ViewModel_DiaryPage>() {
                    new ViewModel_DiaryCover(new DiaryCover())
                };
                Save(pages);
                return pages;
            }
        }

        public static void Save(List<ViewModel_DiaryPage> pages) {
            //Save object as JSON File
            File.WriteAllText(App.SavePath, JsonConvert.SerializeObject(pages, settings));


            FileInfo fileInfo = new FileInfo(App.SavePath);
            Console.WriteLine("JSON File Saved, Size of file " + fileInfo.Length);

        }

        public static void DeleteDiary() {
            if (File.Exists(App.SavePath)) {
                File.Delete(App.SavePath);
                Console.WriteLine("Diary Deleted");
            }
        }



    }
}
