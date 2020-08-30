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
        public int CPQID { get; set; }

        [Column("CheckBoxID")]
        public int CheckBoxID { get; set; }

        [Column("CheckText")]
        public override string Label { get; set; }

        [Column("ResponseType")]
        public string ResponseType { get; set; }

        [Column("CheckBoxValue")]
        public int CheckBoxValue { get; set; }



    }
}
