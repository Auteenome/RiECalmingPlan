using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RiECalmingPlan.Models.JSON {
    public class DiaryEntry {
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
        public string Starter { get; set; }
        public string Body { get; set; }
        public string PhotoLink { get; set; } //Problem with this, see Page_NewDiaryEntry.cs, OnPickPhotoButtonClicked()
        public Double HappinessIndicator { get; set; }

        public DateTime FirstSubmit { get; set; }
        public DateTime LastEdited { get; set; }

    }
}
