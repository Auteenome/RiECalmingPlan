using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("CalmDistressLevelResponse")]
    public class CalmDistressLevelResponse : DistressResponse {
        //Calm, doesn't need distress level type

        //Acute, Moderate, Mild
        [Column("TimeStamp")]
        public override DateTime TimeStamp { get; set; }

        //Terrific – was there anything that helped you feel calm?
        [Column("Response1")]
        public string Response1 { get; set; } = "";

        //Is there anything you will do to keep you feeling this way?
        [Column("Response2")]
        public string Response2 { get; set; } = "";
    }
}
