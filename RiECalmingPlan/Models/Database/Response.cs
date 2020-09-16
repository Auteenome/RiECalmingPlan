using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.Models {
    public class Response : ViewModels.ViewModel_Base {
        [Column("CPQID")]
        public virtual int CPQID { get; set; }
        [Column("QID")]
        public virtual int QID { get; set; }
        [Column("Label")]
        public virtual string Label { get; set; }
        [Column("ResponseType")]
        public virtual string ResponseType { get; set; } //Either a base response from the database or a custom response from the user
        [Column("Override")]
        public virtual string Override {get; set; }
}
}
