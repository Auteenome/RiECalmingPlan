using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class AnswerTemplateSelector : DataTemplateSelector {
        public DataTemplate StepperTemplate { get; set; }
        public DataTemplate CheckBoxTemplate { get; set; }
        public DataTemplate TextResponseTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            switch (((DisplayQuestion)item).Question.QuestionType) {
                case ("Stepper"):
                    return StepperTemplate;
                case ("CheckBox"):
                    return CheckBoxTemplate;
                case ("Text Response"):
                    return TextResponseTemplate;
                default:
                    return TextResponseTemplate;
            }
        }
    }
}
