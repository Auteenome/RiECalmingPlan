using MvvmHelpers;
using RiECalmingPlan.LocalNotifications;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DistressLevel : ViewModel_Base {
        /*
         * This class pulls all responses from the database and slots them in the appropriate distress level group, defined from the question it belongs to.
         * 
         * 1. The user clicks on a specific distress level [Calm, Mild, Moderate, Acute]. This also logs the start of a timestamp.
         * Clicking on a distress level also starts/resets a timer. After [1 minute] has passed without restarting the timer, it will be logged into the database for later use. 
         * 2. The app will then show all responses that belong to questions that belong to one of these distress levels, with included suggestions.
         * 3. The app will also show phone numbers and urls that the user can click on to access these links
         * 
         */
        private string _DistressType;
        private ObservableRangeCollection<Response> _DistressExpressions;
        private ObservableRangeCollection<Response> _DistressResponses;
        private ObservableRangeCollection<Suggestion> _DistressSuggestions;

        public Command<string> FilterResponses { get; private set; }
        public string DistressType { get { return _DistressType; } set { SetProperty(ref _DistressType, value); GenerateDistressExpressions();} }
        public ObservableRangeCollection<Response> DistressExpressions { get { return _DistressExpressions; } set { SetProperty(ref _DistressExpressions, value); } }

        public ObservableRangeCollection<Suggestion> DistressSuggestions { get { return _DistressSuggestions; } set { SetProperty(ref _DistressSuggestions, value); } }
        public ObservableRangeCollection<Response> DistressResponses { get { return _DistressResponses; } set { SetProperty(ref _DistressResponses, value); } }
        public Command<string> CallNumber { get; private set; }
        public Command<string> OpenWebLink { get; private set; }

        /*
         * Previously the user would get a local notification when data has been logged for the use of Distress Graph. 
         * This is redacted because the sound part wasn't removed during a meeting and members found it distracting.
         */

        public ViewModel_DistressLevel() {
            FilterResponses = new Command<string>(FilterByLevel);
            CallNumber = new Command<string>(Call);
            OpenWebLink = new Command<string>(OpenBrowser);
            DistressExpressions = new ObservableRangeCollection<Response>();

        }

        private void Call(string s) {
            if (s.ToString() != null) {
                PhoneDialer.Open(s.ToString());
            }
        }

        // changed method to public static so that it can be accessed elsewhere - maybe this should be moved to a utility class?
        public static void OpenBrowser(string s) {
            if (!string.IsNullOrWhiteSpace(s.ToString())) {
                Browser.OpenAsync("https://" + s, new BrowserLaunchOptions {//Have to add Https:// to the link before opening it so it won't crash
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Color.FromHex("#006738"),//RiE Green(I took the hex for this but i don't know how to access the colour dict for this)
                    PreferredControlColor = Color.White,
                });
            }
        }

        private async void FilterByLevel(string parameter) {
            /*
             * This code is triggered using whichever text is contained within the Distress Level buttons
             * 
             */
            DistressType = parameter;
            Location location;
            IEnumerable<Placemark> placemarks;
            Placemark placemark;
            string locationString = "-";//If the try/catch block fails it will save this NA value to the cell instead

            //Reset current location
            try {
                location = await Geolocation.GetLastKnownLocationAsync().ConfigureAwait(false);
                placemarks = await Geocoding.GetPlacemarksAsync(location).ConfigureAwait(false);
                placemark = placemarks?.FirstOrDefault();
                locationString = placemark.FeatureName + " " + placemark.Thoroughfare + ", " + placemark.Locality + ", " + placemark.AdminArea + ", " + placemark.CountryName + ", " + placemark.PostalCode;

            } catch (FeatureNotSupportedException fnsEx) {
                // Feature not supported on device
                Console.WriteLine(fnsEx);
            } catch (Exception ex) {
                // Handle exception that may have occurred in geocoding
                Console.WriteLine(ex);
            }

            UserInputDistressLevel TimeStamp = new UserInputDistressLevel() {
                DistressLevelType = DistressType,
                StartTime = DateTime.Now,
                Location = locationString
            };

            //Log user's current distress level
            await App.database.AppendUserInputDistressLevel(TimeStamp);

        }


        private async void GenerateDistressExpressions() {
            //Top Half. Most of the work is done in Database class
            DistressExpressions = await App.database.GetDistressExpressions(DistressType);
            GenerateDistressSuggestions();
        }

        private async void GenerateDistressSuggestions() {
            //Bottom Half. Again most of the work is done in Database class
            DistressResponses = await App.database.GetDistressInterventions(DistressType);
            if (DistressType.Equals("Acute") && DistressExpressions.Any(p => p.Override != null && p.Override.Equals("LT-Acute"))) {
                DistressSuggestions = await App.database.GetDistressSuggestions("LT-Acute");
            } else {
                DistressSuggestions = await App.database.GetDistressSuggestions(DistressType);
            }
        }

    }
}
