using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SoccerId.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace SoccerId.Entities
{
    public class User : IdentityUser<int, AppUserLogin, AppUserRole,
    AppUserClaim>
    {
        public User()
        {
            this._Photo = new HashSet<UserPhoto>();
            this._TeamHistory = new HashSet<UserArchiveTeam>();
            this._PlayingPositions = new HashSet<PlayingPosition>();
            this._Chats = new HashSet<Chat>();
            this._PrivateMessages = new HashSet<PrivateMessage>();
            this._AgreedPlayersLists = new HashSet<AgreedPlayersList>();
        }
        
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]        
        public string LastName { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime Birthday { get; set; }
        public virtual UserPhoto _MajorPhoto { get; set; }
        public virtual ICollection<UserPhoto> _Photo { get; set; }        
        public virtual Team _CurrentTeam { get; set; }        
        public virtual ICollection<UserArchiveTeam> _TeamHistory { get; set; }        
        public virtual ICollection<PlayingPosition> _PlayingPositions { get; set; }
        public virtual ICollection<Chat> _Chats { get; set; }
        public virtual ICollection<PrivateMessage> _PrivateMessages { get; set; }
        public virtual ICollection<AgreedPlayersList> _AgreedPlayersLists { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(
                this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }
    }
}