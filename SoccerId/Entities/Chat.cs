using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class Chat
    {
        public Chat()
        {
            this._Participants = new HashSet<User>();
        }
        public int Id { get; set; }
        [Column(TypeName = "DateTime2")]
        
        public DateTime Beginning { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime End { get; set; }
        
        public virtual User _Creator { get; set; }
        
        public virtual  ICollection<User> _Participants { get; set; }
    }
}