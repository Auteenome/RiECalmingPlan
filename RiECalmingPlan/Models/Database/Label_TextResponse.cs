using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("TextResponseLabels")]
    public class Label_TextResponse : NonGeneratedResponse {
        [Column("CPQID")]
        public int CPQID { get; set; }

        [Column("TextResponseID")]
        public int TextResponseID { get; set; }

        [Column("TextResponse")]
        public override string Label { get; set; }

        [Column("ResponseType")]
        public override string ResponseType { get; set; }



    }
}

