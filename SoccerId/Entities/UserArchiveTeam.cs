using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class UserArchiveTeam
    {
        [Key, Column(Order = 0)]
        public int User_Id { get; set; }
        [Key, Column(Order = 1 )]
        public int ArchiveTeam_Id { get; set; }
        
        public virtual User _User { get; set; }        
        public virtual ArchiveTeam _ArchiveTeam { get; set; }        
        [Column(TypeName = "DateTime2")]
        public DateTime Beginning { get; set; }        
        [Column(TypeName = "DateTime2")]
        public DateTime End { get; set; }
    }
}