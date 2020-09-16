using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("TextResponseLabels")]
    public class Label_TextResponse : NonGeneratedResponse {
        [Column("CPQID")]
        public override int CPQID { get; set; }

        [Column("QID")]
        public override int QID { get; set; }

        [Column("Label")]
        public override string Label { get; set; }

        [Column("ResponseType")]
        public override string ResponseType { get; set; }


    }
}

