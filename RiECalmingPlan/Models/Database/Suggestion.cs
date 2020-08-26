using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("Suggestion")]
    public class Suggestion {


        [Column("Level")]
        public string Level { get; set; }

        [Column("Label")]
        public string Label { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("URL")]
        public string URL { get; set; }

    }
}
