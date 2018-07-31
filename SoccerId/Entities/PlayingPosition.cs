using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class PlayingPosition
    {
        public PlayingPosition()
        {
            this._Players = new HashSet<User>();
        }
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string PositionFirstName { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string PositionLastName { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Abbreviation { get; set; }
        public virtual ICollection<User> _Players { get; set; }

    }
}