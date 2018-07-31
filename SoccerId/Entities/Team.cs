using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class ArchiveTeam : BaseTeam
    {

        public ArchiveTeam()
        {
            this.UserArchiveTeam = new HashSet<UserArchiveTeam>();
        }
        
        public virtual ICollection<UserArchiveTeam> UserArchiveTeam { get; set; }
    }


    public class Team : BaseTeam
    {        
        public Team()
        {
            this._Photo = new HashSet<TeamPhoto>();
            this._Players = new HashSet<User>();
            
        }
               
        public virtual ICollection<TeamPhoto> _Photo { get; set; }
        
        public virtual League _CurrentLeague { get; set; }
        
        public virtual User _TeamManager { get; set; }
        
        public virtual ICollection<User> _Players { get; set; }
    }

    public abstract class BaseTeam
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        
        public string TeamName { get; set; }
        [Column(TypeName = "DateTime2")]
        
        public DateTime CreationTime { get; set; }
        public virtual TeamLogo _Logo { get; set; }
    }

}