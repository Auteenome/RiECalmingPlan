using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RiECalmingPlan.Droid.SQLite;
using RiECalmingPlan.SQLite;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace RiECalmingPlan.Droid.SQLite {
    public class SQLite_Android : ISQLite {

        async Task<SQLiteAsyncConnection> ISQLite.ResetDatabase() {
            String databaseName = "Questions.db";
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbFile = Path.Combine(docFolder, databaseName); // FILE PATH TO USE WHEN COPIED

            //Copies and pastes db file from assets folder to above path
            if (File.Exists(dbFile)) {
                File.Delete(dbFile);
                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);//Creates a writestream using the combined filepath as the destination
                await Android.App.Application.Context.Assets.Open(databaseName).CopyToAsync(writeStream);//Copies the assets database to writestream destination
            }

            // Create the connection
            var conn = new SQLiteAsyncConnection(dbFile);
            // Return the database connection
            return conn;
        }

        async Task<SQLiteAsyncConnection> ISQLite.GetConnection() {
            String databaseName = "Questions.db";
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbFile = Path.Combine(docFolder, databaseName); // FILE PATH TO USE WHEN COPIED

            //Copies and pastes db file from assets folder to above path
            if (!File.Exists(dbFile)) {
                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                await Android.App.Application.Context.Assets.Open(databaseName).CopyToAsync(writeStream);
            }

            var path = dbFile;
            // Create the connection
            var conn = new SQLiteAsyncConnection(path);
            // Return the database connection
            return conn;
        }
    }
}