using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Provider;
using Android.Database;
using Android;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Plugin.Permissions;
using Uri = Android.Net.Uri;

namespace RiECalmingPlan.Droid {
    [Activity(Label = "RiECalmingPlan", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Instance = this;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            string savedata = "savedata.json";
            string folderpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string savepath = Path.Combine(folderpath, savedata);


            LoadApplication(new App(savepath));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000; 
        public TaskCompletionSource<string> GetImagePathAsync { get; internal set; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent) {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId) {
                if ((resultCode == Result.Ok) && (intent != null)) {
                    Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    string path = GetFilePath(uri);

                    // Set the Stream as the completion of the Task
                    GetImagePathAsync.SetResult(path);
                }
                else {
                    GetImagePathAsync.SetResult(null);
                }
            }
        }

        private string GetFilePath(Uri uri) {
            string filePath = "";
            //             
            string imageId = DocumentsContract.GetDocumentId(uri);
            string id = imageId.Split(':')[1];
            string[] proj = { MediaStore.Images.Media.InterfaceConsts.Data };
            string sel = MediaStore.Images.Media.InterfaceConsts.Id + "=?";


            using (ICursor cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, proj, sel, new string[] { id }, null)) {
                int columnIndex = cursor.GetColumnIndex(proj[0]);
                if (cursor.MoveToFirst()) {
                    filePath = cursor.GetString(columnIndex);
                }
            }
            return filePath;


        }




    }
}