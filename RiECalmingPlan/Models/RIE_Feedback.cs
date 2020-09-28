using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace RiECalmingPlan.Models
{
    // this is unused legacy code
    public class RIE_Feedback
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int CPQID { get; set; }
        [Indexed]
        public int FeedbackID { get; set; }
        public string Feedback { get; set; }
    }
}
