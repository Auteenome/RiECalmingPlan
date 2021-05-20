using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class UserDiaryTemplateSelector : DataTemplateSelector {
        /*
         * The Diary Pages are sorted into 2 types (Cover and Entry), each having a COMPLETED and EDITING state.
         * 
         * This allows for constant switching between page states
         */

        public DataTemplate CompletedTemplate { get; set; }
        public DataTemplate EditingTemplate { get; set; }
        public DataTemplate CoverTemplate { get; set; }
        public DataTemplate CoverEditingTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            if (item is ViewModel_DiaryCover cover) {
                if (cover.CurrentState == ViewModel_DiaryPage.PageState.EDITING) {
                    return CoverEditingTemplate;
                } else {
                    return CoverTemplate;
                }
            } else if(item is ViewModel_DiaryEntry entry) {
                if (entry.CurrentState == ViewModel_DiaryPage.PageState.EDITING) {
                    return EditingTemplate;
                } else {
                    return CompletedTemplate;
                }
            }
            return null;
        }

    }
}
