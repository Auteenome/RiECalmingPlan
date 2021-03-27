using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models.JSON {
    public static class BackgroundDictionary {

        public static List<string> CoverBackgrounds {
            get {
                return new List<string>() {
                    "bg_morning",
                    "bg_noon",
                    "bg_night"
                };
            }
        }

    }
}
