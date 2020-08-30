using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("StepperLabels")]
    public class Label_Stepper : GeneratedResponse {

        [Column("CPQID")]
        public int CPQID { get; set; }

        [Column("StepperID")]
        public int StepperID { get; set; }

        [Column("StepperText")]
        public override string Label { get; set; }

        [Column("ResponseType")]
        public override string ResponseType { get; set; }

        private int _StepperValue;
        [Column("StepperValue")]
        public int StepperValue { get { return _StepperValue; } set { SetProperty(ref _StepperValue, value); } }




    }
}
