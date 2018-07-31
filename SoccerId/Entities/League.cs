using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class League
    {
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual LeagueLogo _Logo { get; set; }
        public virtual ICollection<LeaguePhoto> _Photo { get; set; }
        
        public virtual ICollection<Team> _Teams { get; set; }
    }
}