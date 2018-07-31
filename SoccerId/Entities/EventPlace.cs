using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerId.Entities
{
    public class EventPlace
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Address { get; set; }
        public byte[] Map { get; set; }
    }
}