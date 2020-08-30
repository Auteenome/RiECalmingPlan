using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    public class Response : ViewModels.ViewModel_Base {
        public virtual string Label { get; set; }
        public virtual string ResponseType { get; set; } //Either a base response from the database or a custom response from the user
    }
}
