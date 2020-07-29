using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    public class UserInputDistressLevel {

        [Column("DistressLevelType")]
        public string DistressLevelType { get; set; }

        [Column("StartTime")]
        public DateTime StartTime { get; set; }



    }
}
