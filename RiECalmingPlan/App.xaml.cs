using System;
using Xamarin.Forms;
using SQLite;
using System.IO;
using RiECalmingPlan.Views;
using RiECalmingPlan.Models;
using Xamarin.Essentials;


namespace RiECalmingPlan {
    public partial class App : Application {

        public static string SavePath;
        public static readonly Database database = new Database();

        // values used for defining a 'small' sized screen
        // used to load 'small' resource dictionary
        // tablets are detected with idiom
        public const int smallWidthResolution = 768;
        public const int smallHeightResolution = 1280;

        public App(string savepath = "")
        {
            InitializeComponent();
            SavePath = savepath;    //Path used for saving to JSON (Different path between android/IOS)

            //Initialise the Application Properties used as flags
            //Application.Current.Properties["TandC_Accepted"] = "NotAgreed"; //Flag to indicate Terms and COnditions
                                                                            //  have been agreed to by a user
            //Application.Current.Properties["userLogin"] = "";   //Stores a user email
            //Application.Current.Properties["usePassword"] = ""; //Stores a user password
            //Application.Current.Properties["loginOK"] = "NotOK"; //Not used yet   
            //Application.Current.Properties["isRegistered"] = "NotRegistered";   //Flag to indicate that a user exists and
                                                                                // user details are stored in the database
                                                                                //Needs additional vlaidation in the
                                                                                //  Registration page as no checking is done
                                                                                //  for a username as a valid email

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;                                // get device display info
            Application.Current.Properties["widthResolution"] = mainDisplayInfo.Width;          // get width as double
            Application.Current.Properties["heightResolution"] = mainDisplayInfo.Height;        // get height as double

            // uncomment this for testing
            AppPreferences.TermsAndConditionsAccepted = false;

            LoadStyles();
            MainPage = LoadMainPage();
        }

        private NavigationPage LoadMainPage()
        {
            // decides whether to navigate to menu page or login page
            if (!AppPreferences.AccountCreated || !AppPreferences.TermsAndConditionsAccepted)  // if no account, or t&c not accepted
            {
                return new NavigationPage(new Page_Login()) 
                {
                    Style = this.Resources["NavBarStyle"] as Style      // references the resource dictionary, and loads the navbar style
                };
            }
            else
            {
                return new NavigationPage(new Pages.Page_Menu()) 
                {
                    Style = this.Resources["NavBarStyle"] as Style      // references the resource dictionary, and loads the navbar style
                };
            }
        }

        private void LoadStyles()
        {
            Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Base.SharedDictionary);     // load base style

            // choose which file to override with based on screen size
            if (Device.Idiom == TargetIdiom.Tablet)
                Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Tablet.SharedDictionary);    // merge Dictionary_Tablet with main
            else if (BoolIsSmallDevice())
                Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Small.SharedDictionary);     // merge Dictionary_Small with main
            else
                Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Default.SharedDictionary);   // merge Dictionary_Default with main
        }

        private static bool BoolIsSmallDevice()
        {
            // method will be used to detect if device is 'small'
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            var height = mainDisplayInfo.Height;

            return (width <= smallWidthResolution && height <= smallHeightResolution);
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }    
    }
}
