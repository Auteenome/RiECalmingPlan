using MvvmHelpers;
using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RiECalmingPlan.ViewModels {
    public class DistressLevelViewModel : ViewModel_Base {
        /*
         * This class pulls all responses from the database and slots them in the appropriate distress level group, defined from the question it belongs to.
         * 
         * 
         */
        private string _DistressType;
        private ObservableRangeCollection<Response> _DistressExpressions;

        public Command<string> FilterResponses { get; private set; }
        public string DistressType { get { return _DistressType; } set { SetProperty(ref _DistressType, value); GenerateDistressExpressions(); } }
        public ObservableRangeCollection<Response> DistressExpressions { get { return _DistressExpressions; } set { SetProperty(ref _DistressExpressions, value); } }


        public DistressLevelViewModel() {
            FilterResponses = new Command<string>(FilterByLevel);
            DistressExpressions = new ObservableRangeCollection<Response>();
        }

        private void FilterByLevel(string parameter) {
            DistressType = parameter;
        }

        private async void GenerateDistressExpressions() {
            DistressExpressions = await App.database.GetDistressLevelViewModelList(DistressType);
            Console.WriteLine("Viewmodel Expression Size: " + DistressExpressions.Count);
        }
    }
}
