using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class UserDiaryTemplateSelector : DataTemplateSelector {
        public DataTemplate CompletedTemplate { get; set; }
        public DataTemplate EditingTemplate { get; set; }
        public DataTemplate NewSpaceTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            switch (((ViewModel_DiaryEntry)item).CurrentState) {
                case ViewModel_DiaryEntry.DiaryEntryState.COMPLETED:
                    return CompletedTemplate;
                case ViewModel_DiaryEntry.DiaryEntryState.EDITING:
                    return EditingTemplate;
                case ViewModel_DiaryEntry.DiaryEntryState.NEWSPACE:
                    return NewSpaceTemplate;
                default:
                    return NewSpaceTemplate;

            }
        }

    }
}
