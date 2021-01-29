using MvvmHelpers;
using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RiECalmingPlan.Models.JSON {
    public class DiaryEntry : ViewModel_Base {
        /*
         * Model class that holds a single Diary Entry (Page of a diary) and consists of the following in order:
         * 
         * 1. Starter Question/Statement (This is pulled from the local database instead of the JSON file when the user is expected to make a new
         *      diary entry, but saved here so saved entries are easier to pull)
         * 2. Entry Body (For the user to write their response)
         * 3. Photo link (As a JSON File, this will be used as a string path to find the image for that entry)
         * 4. Happiness Indicator (Conclude the diary entry with a scale 1-10 on how their day was)
         * 
         * Other variables that will be used are:
         * 1. Date of first submission
         * 2. Date last edited (Which may be merged with Date of last edited)
         *
         */
        private int _Starter;
        private string _Body;
        private ObservableRangeCollection<string> _PhotoLinks;
        private Double _HappinessIndicator;

        private DateTime _FirstSubmit;
        private DateTime _LastEdited;

        public int Starter { get { return _Starter; } set { SetProperty(ref _Starter, value); } }
        public string Body { get { return _Body; } set { SetProperty(ref _Body, value); } }
        public ObservableRangeCollection<string> PhotoLinks { get { return _PhotoLinks; } set { SetProperty(ref _PhotoLinks, value); } }
        public Double HappinessIndicator { get { return _HappinessIndicator; } set { SetProperty(ref _HappinessIndicator, value); } }

        public DateTime FirstSubmit { get { return _FirstSubmit; } set { SetProperty(ref _FirstSubmit, value); } }
        public DateTime LastEdited { get { return _LastEdited; } set { SetProperty(ref _LastEdited, value); } }



    }
}
