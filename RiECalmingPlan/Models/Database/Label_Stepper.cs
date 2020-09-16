using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("StepperLabels")]
    public class Label_Stepper : GeneratedResponse {

        [Column("CPQID")]
        public override int CPQID { get; set; }

        [Column("QID")]
        public override int QID { get; set; }

        [Column("Label")]
        public override string Label { get; set; }

        [Column("ResponseType")]
        public override string ResponseType { get; set; }

        private int _Value;
        [Column("Value")]
        public int Value { get { return _Value; } set { SetProperty(ref _Value, value); } }

        [Column("Override")]
        public override string Override { get; set; }



    }
}
