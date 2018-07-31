using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class LeaguePhoto : Photo
    {

    }
    public class TeamPhoto : Photo
    {

    }
    public class UserPhoto : Photo
    {

    }
    public class TeamLogo : Logo
    {
        
    }
    public class LeagueLogo : Logo
    {

    }
    public class Logo : Photo
    {
        public byte[] SmallImage { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        
        public byte[] Image { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string PhotoName { get; set; }
    }
}