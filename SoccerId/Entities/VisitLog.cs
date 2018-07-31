using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class VisitLog
    {
        public int Id { get; set; }        
        public virtual User _User { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Beginning { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime End { get; set; }
    }
}