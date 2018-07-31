using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class PrivateMessage : BaseMessage
    {
        public PrivateMessage()
        {
            this._Recipients = new HashSet<User>();
        }
        
        public virtual ICollection<User> _Recipients { get; set; }
    }

    public class ChatMessage : BaseMessage
    {
        
        public virtual Chat _Chat { get; set; }
    }

    public class BaseMessage
    {
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar")]
        [MaxLength]
        public string Message { get; set; }
        [Column(TypeName="DateTime2")]
        
        public DateTime SendDateTime { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ReadDateTime { get; set; }
        
        public virtual User Sender { get; set; }
    }
}