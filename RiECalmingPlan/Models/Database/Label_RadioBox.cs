using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("RadioBoxLabels")]
    public class Label_RadioBox : Response {

        [Column("CPQID")]
        public int CPQID { get; set; }

        [Column("RadioBoxID")]
        public int RadioBoxID { get; set; }

        [Column("RadioBoxText")]
        public string RadioBoxText { get; set; }

        [Column("RadioBoxValue")]
        public string RadioBoxValue { get; set; }



    }
}
