using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class DisplayQuestion : INotifyPropertyChanged{

        public Question Question { get; set; }
        public List<Response> Responses { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DisplayQuestion(Question question, List<Response> response) {
            this.Question = question;
            this.Responses = response;
        }
    }
}
