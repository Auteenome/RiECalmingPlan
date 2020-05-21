using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class DisplayQuestion {

        public Question Question { get; set; }
        //Non Text Related Responses, since checkbox questions may have 'other' text responses, these are usually preemptively made in the database
        public List<GeneratedResponse> GeneratedResponses { get; set; }
        //List of text related responses
        public List<NonGeneratedResponse> NonGeneratedResponses { get; set; }

        public DisplayQuestion(Question question, List<GeneratedResponse> generatedResponses, List<NonGeneratedResponse> nonGeneratedResponses) {
            this.Question = question;
            this.GeneratedResponses = generatedResponses;
            this.NonGeneratedResponses = nonGeneratedResponses;
        }
    }
}
