using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class DisplayQuestion {

        public Question Question { get; set; }
        public List<Response> Response { get; set; }

        public DisplayQuestion(Question question, List<Response> response) {
            this.Question = question;
            this.Response = response;
        }

    }
}
