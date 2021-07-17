using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    public static class DistressType {
        /*
         * Just an easier way of mapping Distress Types to a numbered ranking system
         * 
         * 
         * 
         */


        public static string DistressTypeValue(int i) {
            switch (i) {
                case 1:
                    return "Calm";
                case 2:
                    return "Mild";
                case 3:
                    return "Moderate";
                case 4:
                    return "Acute";
                default:
                    return "Calm";
            }
        }

        public static int DistressTypeValue(string s) {
            switch (s) {
                case "Calm":
                    return 1;
                case "Mild":
                    return 2;
                case "Moderate":
                    return 3;
                case "Acute":
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
