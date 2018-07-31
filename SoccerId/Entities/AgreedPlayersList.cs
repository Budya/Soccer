using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class AgreedPlayersList
    {
        public AgreedPlayersList()
        {
            this._Users = new HashSet<User>();
        }
        public int Id { get; set; }
        
        public virtual TeamEvent _TeamEvent { get; set; }
        public virtual ICollection<User> _Users { get; set; }
        
        public virtual Team  _Team { get; set; }
    }
}