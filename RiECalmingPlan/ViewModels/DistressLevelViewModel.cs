using MvvmHelpers;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace RiECalmingPlan.ViewModels {
    public class DistressLevelViewModel : ViewModel_Base {
        /*
         * This class pulls all responses from the database and slots them in the appropriate distress level group, defined from the question it belongs to.
         * 
         * 1. The user clicks on a specific distress level [Calm, Mild, Moderate, Acute]. This also logs the start of a timestamp.
         * Clicking on a distress level also starts/resets a timer. After [1 minute] has passed without restarting the timer, it will be logged into the database for later use. 
         * 2. The app will then show all responses that belong to questions that belong to one of these distress levels, with included suggestions.
         * 
         * 
         */
        private string _DistressType;
        private ObservableRangeCollection<Response> _DistressExpressions;
        private ObservableRangeCollection<Suggestion> _DistressSuggestions;

        public Command<string> FilterResponses { get; private set; }
        public string DistressType { get { return _DistressType; } set { SetProperty(ref _DistressType, value); GenerateDistressExpressions(); GenerateDistressSuggestions(); } }
        public ObservableRangeCollection<Response> DistressExpressions { get { return _DistressExpressions; } set { SetProperty(ref _DistressExpressions, value); } }

        public ObservableRangeCollection<Suggestion> DistressSuggestions { get { return _DistressSuggestions; } set { SetProperty(ref _DistressSuggestions, value); } }

        private Timer timer = new Timer(60 * 1000);
        private UserInputDistressLevel TimeStamp;


        public DistressLevelViewModel() {
            FilterResponses = new Command<string>(FilterByLevel);
            DistressExpressions = new ObservableRangeCollection<Response>();
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false;
        }

        private void FilterByLevel(string parameter) {
            /*
             * This code is triggered using whichever text is contained within the Distress Level buttons
             * 
             */
            DistressType = parameter;
            //Timer reset
            timer.Interval = 60 * 1000;
            timer.Start();
            //Reset current log
            TimeStamp = new UserInputDistressLevel() { DistressLevelType = DistressType, StartTime = DateTime.Now };
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            Console.WriteLine("Timestamp added");
            await App.database.AppendUserInputDistressLevel(TimeStamp);
            timer.Stop();
        }

        private async void GenerateDistressExpressions() {
            DistressExpressions = await App.database.GetDistressExpressions(DistressType);
        }

        private async void GenerateDistressSuggestions() {
            DistressSuggestions = await App.database.GetDistressSuggestions(DistressType);
        }

        public void StopTimer() {
            timer.Stop();
        }
    }
}
