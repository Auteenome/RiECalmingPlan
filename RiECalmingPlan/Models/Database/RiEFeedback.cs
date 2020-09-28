using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("RiEFeedback")]
    public class RiEFeedback {

        [PrimaryKey, AutoIncrement, Column("CPQID")]
        public int CPQID { get; set; }

        [Column("FeedbackText")]
        public string FeedbackText { get; set; }
    }
}
