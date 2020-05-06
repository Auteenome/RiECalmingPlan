using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("CheckBoxLabels")]
    public class Label_CheckBox : Response {

        [Column("CPQID")]
        public int CPQID { get; set; }

        [Column("CheckBoxID")]
        public int CheckBoxID { get; set; }

        [Column("CheckText")]
        public string CheckText { get; set; }

        [Column("CheckBoxValue")]
        public bool CheckBoxValue { get; set; }





    }
}
