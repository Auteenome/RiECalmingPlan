using Foundation;
using RiECalmingPlan.iOS.SQLite;
using RiECalmingPlan.SQLite;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_IOS))]
namespace RiECalmingPlan.iOS.SQLite {
    public class SQLite_IOS : ISQLite {

        async Task<SQLiteAsyncConnection> ISQLite.ResetDatabase() {
            String databaseName = "Questions.db";

            //This path has to be beyond the Personal Folder (Unlike Android)
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            
            if (!Directory.Exists(libFolder)) {
                Directory.CreateDirectory(libFolder);
            }

            string dbFile = Path.Combine(libFolder, databaseName);


            //Deletes existing database file
            if (File.Exists(dbFile)) {
                File.Delete(dbFile);

                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                string source = NSBundle.MainBundle.PathForResource("Questions", "db");
                FileStream sourceStream = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Read);
                await sourceStream.CopyToAsync(writeStream);

            }

            var conn = new SQLiteAsyncConnection(dbFile);

            return conn;
        }

        async Task<SQLiteAsyncConnection> ISQLite.GetConnection() {
            String databaseName = "Questions.db";

            //This path has to be beyond the Personal Folder (Unlike Android)
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder)) {
                Directory.CreateDirectory(libFolder);
            }

            string dbFile = Path.Combine(libFolder, databaseName);

            //Create a new folder if the one defined does not exist
            if (!File.Exists(dbFile)) {
                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                string source = NSBundle.MainBundle.PathForResource("Questions", "db");
                FileStream sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read);
                await sourceStream.CopyToAsync(writeStream);
            }

            var conn = new SQLiteAsyncConnection(dbFile);

            return conn;
        }





    }
}