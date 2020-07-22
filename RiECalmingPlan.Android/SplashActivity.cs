using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RiECalmingPlan.Droid {
    [Activity(Label = "SplashActivity", Icon = "@mipmap/icon", Theme = "@style/SplashTheme", MainLauncher = false)]
    // change icon = to change the app icon
    public class SplashActivity : Activity {
        // activity displays a splash image while app loadup, before loading the main activity
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.AddFlags(ActivityFlags.SingleTop);
            StartActivity(intent);
            Finish();
        }
    }
}