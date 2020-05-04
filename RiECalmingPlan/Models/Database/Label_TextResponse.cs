using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("TextResponseLabels")]
    public class TextResponseLabel : Response {
        [Column("CPQID")]
        public int CPQID { get; set; }

        [Column("TextResponse")]
        public string TextResponse { get; set; }
    }
}

