using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("CheckBoxLabels")]
    public class Label_CheckBox : GeneratedResponse {

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
