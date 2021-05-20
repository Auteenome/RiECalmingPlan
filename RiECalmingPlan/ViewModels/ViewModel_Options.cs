using RiECalmingPlan.Models.JSON;
using RiECalmingPlan.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_Options : ViewModel_Base {

        private int _DBSize;
        private int _DiarySize;

        public int DBSize { get { return _DBSize; } set { SetProperty(ref _DBSize, value); } }
        public int DiarySize { get { return _DiarySize; } set { SetProperty(ref _DiarySize, value); } }

        public Command Command_ResetDatabase { get; }
        public Command Command_ResetDiary { get; }

        public ViewModel_Options() {
            Command_ResetDatabase = new Command(ResetDatabase);
            Command_ResetDiary = new Command(ResetDiary);
            RefreshInfo();
        }

        private void RefreshInfo() {
            //Just pulls the sizes of the respective files

            //Database
            if (File.Exists(DependencyService.Get<ISQLite>().GetPath())) {
                FileInfo databaseSize = new FileInfo(DependencyService.Get<ISQLite>().GetPath());
                DBSize = (int)databaseSize.Length;
            }

            //Diary
            if (File.Exists(App.SavePath)) {
                FileInfo fileInfo = new FileInfo(App.SavePath);
                DiarySize = (int)fileInfo.Length;
            }
        }

        private void ResetDatabase() {
            //Replaces the current database in usable mobile app space with the base database in package space.
            App.database.ResetConnection();
            Console.WriteLine("Database connection reset");
            RefreshInfo();
        }

        private void ResetDiary() {
            //Deletes the diary. Opening the diary part of the app still creates a new diary file anyway.
            UserDiaryFileController.DeleteDiary();
            Console.WriteLine("Diary Deleted");
            RefreshInfo();
        }

    }
}
