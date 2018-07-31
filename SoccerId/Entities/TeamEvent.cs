using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class TeamEvent
    {
        public int Id { get; set; }
        [Column(TypeName = "DateTime2")]
        
        public DateTime DateTime { get; set; }
        
        public virtual EventType _Type { get; set; }
        
        public virtual EventPlace _Place { get; set; }
        
        public virtual Team _Team1 { get; set; }
        
        public virtual Team _Team2 { get; set; }
    }
}