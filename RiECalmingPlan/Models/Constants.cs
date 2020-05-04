using System;
using Xamarin.Forms;

namespace RiECalmingPlan.Models
{
    
     public class Constants
    {
        public static bool IsDev = true;
        //Whether we are in development or in release code
        //  Not used at the moment

        //This allows colour setting on both ios and Android
        public static Color BackgroundColor = Color.FromRgb(58, 153, 225);
        public static Color MainTextColor = Color.White;

        //OK for iPhone but should be larger for tablet
        public static int LoginIconHeight = 120;
    }
}
