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
        /*
         * DisplayQuestion in its own sense is a glorified Model object that holds a singular row of Question Table information,
         * and multiple Response Table information for the question it belongs to.
         * 
         * This viewmodel also accounts for Non-Generated Responses (Text responses) but is not shown in the app since those questions
         * were redacted from the database.
         * 
         * 
         * Each Display Question also allows for the following commands (Add/Delete/DisplayFeedback) that are bindable to its respective View.
         * 
         */



        private Question _Question;
        private ObservableRangeCollection<GeneratedResponse> _GeneratedResponses;
        private ObservableRangeCollection<NonGeneratedResponse> _NonGeneratedResponses;
        private string _OtherText;
        private readonly string _RiEFeedback;


        public Question Question { get { return _Question; } set { SetProperty(ref _Question, value); } }
        //Non Text Related Responses, since checkbox questions may have 'other' text responses, these are usually preemptively made in the database
        public ObservableRangeCollection<GeneratedResponse> GeneratedResponses { get { return _GeneratedResponses; } set { SetProperty(ref _GeneratedResponses, value); } }
        //List of text related responses
        public ObservableRangeCollection<NonGeneratedResponse> NonGeneratedResponses { get { return _NonGeneratedResponses; } set { SetProperty(ref _NonGeneratedResponses, value); } }

        public string OtherText { get { return _OtherText; } set { SetProperty(ref _OtherText, value); } }
        public Command<string> AddResponseCommand { get; private set; } 
        public Command<Response> DeleteResponseCommand { get; private set; }
        public Command DisplayRiEFeedbackCommand { get; private set; }

        public DisplayQuestion(Question question, List<GeneratedResponse> generatedResponses, List<NonGeneratedResponse> nonGeneratedResponses, string RiEfb) {
            Question = question;
            GeneratedResponses = new ObservableRangeCollection<GeneratedResponse>();
            NonGeneratedResponses = new ObservableRangeCollection<NonGeneratedResponse>();
            GeneratedResponses.ReplaceRange(generatedResponses);
            NonGeneratedResponses.ReplaceRange(nonGeneratedResponses);
            AddResponseCommand = new Command<string>(AddResponse);
            DeleteResponseCommand = new Command<Response>(DeleteResponse);
            _OtherText = string.Empty;

            _RiEFeedback = RiEfb;
            DisplayRiEFeedbackCommand = new Command(DisplayRiEFeedback);
        }

        public async void AddResponse(string s) {
            if (!string.IsNullOrWhiteSpace(s)) {
                Console.WriteLine("Adding Response " + s);
                if (Question.QuestionType.Equals("CheckBox")) {
                    Label_CheckBox checkbox = new Label_CheckBox() { CPQID = Question.CPQID, QID = GeneratedResponses.Count + 1, Label = s, ResponseType = "Custom", Value = 1 };
                    GeneratedResponses.Add(checkbox);
                    await App.database.AppendCheckBoxResponse(checkbox);
                } else if (Question.QuestionType.Equals("Stepper")) {
                    Label_Stepper stepper = new Label_Stepper() { CPQID = Question.CPQID, QID = GeneratedResponses.Count + 1, Label = s, ResponseType = "Custom", Value = 0 };
                    GeneratedResponses.Add(stepper);
                    await App.database.AppendStepperResponse(stepper);
                } else {
                    Label_TextResponse textResponse = new Label_TextResponse() { CPQID = Question.CPQID, QID = NonGeneratedResponses.Count + 1, Label = s, ResponseType = "Custom" };
                    NonGeneratedResponses.Add(textResponse);
                    await App.database.AppendTextResponse(textResponse);
                }
            }
            OtherText = string.Empty;
        }

        public async void DeleteResponse(Response x) {
            Console.WriteLine("Deleting Response " + x);
            if (x is GeneratedResponse g) {
                GeneratedResponses.Remove(g);
                if (Question.QuestionType.Equals("Stepper")) {
                    await App.database.DeleteStepperResponse((Label_Stepper)g);
                }
                else if (Question.QuestionType.Equals("CheckBox")) {
                    await App.database.DeleteCheckboxResponse((Label_CheckBox)g);
                }
            }
            else if (x is NonGeneratedResponse ng) {
                NonGeneratedResponses.Remove(ng);
                await App.database.DeleteTextResponse((Label_TextResponse)ng);
            }
        }

        public async void DisplayRiEFeedback()
        {
            if (string.IsNullOrEmpty(_RiEFeedback))
                await Application.Current.MainPage.DisplayAlert("Clinician Feedback", "No feedback is available", "Ok");
            else
                await Application.Current.MainPage.DisplayAlert("Clinician Feedback", _RiEFeedback.ToString(), "Ok");
        }
    }
}
