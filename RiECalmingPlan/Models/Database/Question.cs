using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("Questions")]
    public class Question {

        [PrimaryKey, AutoIncrement, Column("CPQID")]
        public int CPQID { get; set; }

        [Column("QuestionText")]
        public string QuestionText { get; set; }

        [Column("QuestionCarePlanArea")]
        public string QuestionCarePlanArea { get; set; }

        [Column("QuestionType")]
        public string QuestionType { get; set; }

        [Column("DistressLevelType")]
        public string DistressLevelType { get; set; }

    }
}
