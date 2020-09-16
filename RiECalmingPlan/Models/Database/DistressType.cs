using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    public static class DistressType {

        public static string DistressTypeValue(int i) {
            switch (i) {
                case 0:
                    return "Calm";
                case 1:
                    return "Mild";
                case 2:
                    return "Moderate";
                case 3:
                    return "Acute";
                default:
                    return "Calm";
            }
        }

        public static int DistressTypeValue(string s) {
            switch (s) {
                case "Calm":
                    return 0;
                case "Mild":
                    return 1;
                case "Moderate":
                    return 2;
                case "Acute":
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
