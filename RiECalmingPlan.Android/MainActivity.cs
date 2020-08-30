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
using Android.Support.V4.App;
using Plugin.Permissions;
using Uri = Android.Net.Uri;
using Plugin.Fingerprint;

namespace RiECalmingPlan.Droid {
    [Activity(Label = "Auteenome", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState) {
            CrossFingerprint.SetCurrentActivityResolver(() => this);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            global::Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");

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
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000; 
        public TaskCompletionSource<string> GetImagePathAsync { get; internal set; }

    }
}