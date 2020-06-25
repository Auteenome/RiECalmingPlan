using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class DistressCategorySelector : DataTemplateSelector {

        public DataTemplate CalmTemplate { get; set; }
        public DataTemplate NonCalmTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            if (item is NonCalmDistressLevelResponse) {
                return NonCalmTemplate;
            } else {
                return CalmTemplate;
            }
        }
    }
}
