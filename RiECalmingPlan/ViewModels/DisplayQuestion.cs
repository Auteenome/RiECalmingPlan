using MvvmHelpers;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RiECalmingPlan.ViewModels {
    public class DisplayQuestion : ViewModel_Base {

        private Question _Question;
        private ObservableRangeCollection<GeneratedResponse> _GeneratedResponses;
        private ObservableRangeCollection<NonGeneratedResponse> _NonGeneratedResponses;
        private string _OtherText;


        public Question Question { get { return _Question; } set { SetProperty(ref _Question, value); } }
        //Non Text Related Responses, since checkbox questions may have 'other' text responses, these are usually preemptively made in the database
        public ObservableRangeCollection<GeneratedResponse> GeneratedResponses { get { return _GeneratedResponses; } set { SetProperty(ref _GeneratedResponses, value); } }
        //List of text related responses
        public ObservableRangeCollection<NonGeneratedResponse> NonGeneratedResponses { get { return _NonGeneratedResponses; } set { SetProperty(ref _NonGeneratedResponses, value); } }

        public string OtherText { get { return _OtherText; } set { SetProperty(ref _OtherText, value); } }
        public Command<string> AddResponseCommand { get; private set; } 

        public DisplayQuestion(Question question, List<GeneratedResponse> generatedResponses, List<NonGeneratedResponse> nonGeneratedResponses) {
            Question = question;
            GeneratedResponses = new ObservableRangeCollection<GeneratedResponse>();
            NonGeneratedResponses = new ObservableRangeCollection<NonGeneratedResponse>();
            GeneratedResponses.ReplaceRange(generatedResponses);
            NonGeneratedResponses.ReplaceRange(nonGeneratedResponses);
            AddResponseCommand = new Command<string>(AddResponse);
            _OtherText = string.Empty;
        }

        public async void AddResponse(string s) {
            if (!string.IsNullOrWhiteSpace(s)) {
                Console.WriteLine("Adding Response " + s);
                if (Question.QuestionType.Equals("CheckBox")) {
                    Label_CheckBox checkbox = new Label_CheckBox() { CPQID = Question.CPQID, CheckBoxID = GeneratedResponses.Count + 1, Label = s, CheckBoxValue = 1 };
                    GeneratedResponses.Add(checkbox);
                    await App.database.AppendCheckBoxResponse(checkbox);
                } else if (Question.QuestionType.Equals("Stepper")) {
                    Label_Stepper stepper = new Label_Stepper() { CPQID = Question.CPQID, StepperID = GeneratedResponses.Count + 1, Label = s, StepperValue = 0 };
                    GeneratedResponses.Add(stepper);
                    await App.database.AppendStepperResponse(stepper);
                } else {
                    Label_TextResponse textResponse = new Label_TextResponse() { CPQID = Question.CPQID, TextResponseID = NonGeneratedResponses.Count + 1, Label = s };
                    NonGeneratedResponses.Add(textResponse);
                    await App.database.AppendTextResponse(textResponse);
                }
            }
            OtherText = string.Empty;
        }
    }
}
