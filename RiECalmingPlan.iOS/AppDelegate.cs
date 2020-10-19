using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using RiECalmingPlan.iOS.LocalNotifications;
using UIKit;
using UserNotifications;

namespace RiECalmingPlan.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            global::Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            global::Xamarin.Forms.Forms.Init();


            UNUserNotificationCenter.Current.Delegate = new IOSNotificationReceiver();

            string savedata = "savedata.json";
            string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library");
            string savepath = Path.Combine(folderpath, savedata);
            LoadApplication(new App(savepath));

            return base.FinishedLaunching(app, options);
        }

        //#region [Error handling]
        ////Credit: Peter Norman.
        ////https://peterno.wordpress.com/2015/04/15/unhandled-exception-handling-in-ios-and-android-with-xamarin/
        ////Minor compile fixes by David McCurley.

        //private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        //{
        //    var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
        //    LogUnhandledException(newExc);
        //}

        //private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        //{
        //    var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
        //    LogUnhandledException(newExc);
        //}

        //internal static void LogUnhandledException(Exception exception)
        //{
        //    try
        //    {
        //        const string errorFileName = "Fatal.log";
        //        var libraryPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Resources); // iOS: Environment.SpecialFolder.Resources
        //        var errorFilePath = Path.Combine(libraryPath, errorFileName);
        //        var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
        //        DateTime.Now, exception.ToString());
        //        File.WriteAllText(errorFilePath, errorMessage);

        //        // Log to Android Device Logging.
        //        //Android.Util.Log.Error("Crash Report", errorMessage);
        //    }
        //    catch
        //    {
        //        // just suppress any error logging exceptions
        //    }
        //}

        ///// <summary>
        //// If there is an unhandled exception, the exception information is diplayed 
        //// on screen the next time the app is started (only in debug configuration)
        ///// </summary>
        //[Conditional("DEBUG")]
        //private static void DisplayCrashReport()
        //{
        //    const string errorFilename = "Fatal.log";
        //    var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
        //    var errorFilePath = Path.Combine(libraryPath, errorFilename);

        //    if (!File.Exists(errorFilePath))
        //    {
        //        return;
        //    }

        //    var errorText = File.ReadAllText(errorFilePath);
        //    var alertView = new UIAlertView("Crash Report", errorText, null, "Close", "Clear") { UserInteractionEnabled = true };
        //    alertView.Clicked += (sender, args) => {
        //        if (args.ButtonIndex != 0)
        //        {
        //            File.Delete(errorFilePath);
        //        }
        //    };
        //    alertView.Show();
        //}
        //#endregion
    }
}
