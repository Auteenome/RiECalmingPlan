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

        public App(string savepath = "") {
            InitializeComponent();

            SavePath = savepath;//Path used for saving to JSON (Different path between android/IOS)

            //This is the first page actioned
            //Set the Login page to start the app instead of the Main Page

            //Set the database path and connection to use for the entire app

            //Check to see if the Datbase exists - If so, the user has initiated the App,
            //SO    Go to the login page to sign in the user
            //      (that will then set a Navigation Path starting at the Calm Plan Menu as the Root Page)
            //OR    The app has not been initiated, so go to Terms and COnditions and Registration

            //Initialise the Application Properties used as flags
            Application.Current.Properties["TandC_Accepted"] = "NotAgreed"; //Flag to indicate Terms and COnditions
                                                                            //  have been agreed to by a user
            Application.Current.Properties["userLogin"] = "";   //Stores a user email
            Application.Current.Properties["usePassword"] = ""; //Stores a user password
            Application.Current.Properties["loginOK"] = "NotOK"; //Not used yet   
            Application.Current.Properties["isRegistered"] = "NotRegistered";   //Flag to indicate that a user exists and
                                                                                // user details are stored in the database
                                                                                //Needs additional vlaidation in the
                                                                                //  Registration page as no checking is done
                                                                                //  for a username as a valid email
            Application.Current.Properties["setMenuRootPage"] = true;   //This will be used to see if CalmingPlanMenuPage
                                                                        //  will be set as the new Navigation root page.
                                                                        //  On the initial push to CalmingPLanMenuPage it
                                                                        //  is true and will set it as the new Navigation root 
                                                                        //  It will then be set false and subsequent arrivals
                                                                        //  at CalmingPLanMenuPage will not be affected.

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;                                // get device display info
            Application.Current.Properties["widthResolution"] = mainDisplayInfo.Width;          // get width as double
            Application.Current.Properties["heightResolution"] = mainDisplayInfo.Height;        // get height as double

            loadStyles();
            MainPage = loadMainPage();  // will load main menu or login screen based on application property 'setmenurootpage'
        }

        // method will be used to detect if device is 'small'
        private static bool boolIsSmallDevice()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            var height = mainDisplayInfo.Height;

            return (width <= smallWidthResolution && height <= smallHeightResolution);
        }

        private NavigationPage loadMainPage()
        {
            // decides whether to navigate to menu page or login page
            if ((bool)Application.Current.Properties["setMenuRootPage"])
            {
                return new NavigationPage(new Page_Menu())
                {
                    Style = this.Resources["NavBarStyle"] as Style      // references the resource dictionary, and loads the navbar style
                };
            }
            else
            {
                return new NavigationPage(new Page_Login())
                {
                    Style = this.Resources["NavBarStyle"] as Style      // references the resource dictionary, and loads the navbar style
                };
            }
        }

        private void loadStyles()
        {
            Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Base.SharedDictionary);     // load base style

            // choose which file to override with based on screen size
            if (Device.Idiom == TargetIdiom.Tablet)
                Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Tablet.SharedDictionary);    // merge Dictionary_Tablet with main
            else if (boolIsSmallDevice())
                Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Small.SharedDictionary);     // merge Dictionary_Small with main
            else
                Dictionary_Main.MergedDictionaries.Add(ResourceDictionaries.Dictionary_Default.SharedDictionary);   // merge Dictionary_Default with main
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }    
    }
}
