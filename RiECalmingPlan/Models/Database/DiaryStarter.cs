using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    [Table("DiaryStarters")]
    public class DiaryStarter {
        [Column("Label")]
        public string Label { get; set; }

        public override string ToString() {
            return Label;
        }
    }
}
