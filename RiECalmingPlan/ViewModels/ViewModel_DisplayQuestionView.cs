using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DisplayQuestionView : ViewModel_Base {
        private ObservableRangeCollection<DisplayQuestion> _Questions;

        public ObservableRangeCollection<DisplayQuestion> Questions { get { return _Questions; } set { SetProperty(ref _Questions, value); } }

        public ViewModel_DisplayQuestionView() {
            LoadQuestions();
        }

        public async void LoadQuestions() {
            Questions = new ObservableRangeCollection<DisplayQuestion>();
            Questions.AddRange(await App.database.GetDisplayQuestionList().ConfigureAwait(false));
            Questions.Add(new DisplayQuestion() { Question = new Models.Question() { QuestionType = "Last Slide" } });
        }
    }
}
