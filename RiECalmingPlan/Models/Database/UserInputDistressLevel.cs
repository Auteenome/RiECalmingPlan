using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RiECalmingPlan.Models {
    public class UserInputDistressLevel {

        [Column("DistressLevelType")]
        public string DistressLevelType { get; set; }

        [Column("StartTime")]
        public DateTime StartTime { get; set; }

        [Column("Location")]
        public string Location { get; set; }
    }
}
