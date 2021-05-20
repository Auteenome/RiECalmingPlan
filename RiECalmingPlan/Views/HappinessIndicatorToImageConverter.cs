using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RiECalmingPlan.Views {
    public class HappinessIndicatorToImageConverter : IValueConverter {
        /*
         * Converts a number from 0 to 1 into an emoji essentially
         * 
         * 
         */



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string imageSource;
            switch ((Double)value) {
                case Double n when (n < 1 && n >= 0.8):
                    imageSource = "expression_VeryHappy.png";
                    break;
                case Double n when (n < 0.8 && n >= 0.6):
                    imageSource = "expression_Happy.png";
                    break;
                case Double n when (n < 0.6 && n >= 0.4):
                    imageSource = "expression_Neutral.png";
                    break;
                case Double n when (n < 0.4 && n >= 0.2):
                    imageSource = "expression_Unhappy.png";
                    break;
                case Double n when(n < 0.2 && n >= 0):
                    imageSource = "expression_VeryUnhappy.png";
                    break;
                default:
                    imageSource = "expression_Neutral.png";
                    break;

            }

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
