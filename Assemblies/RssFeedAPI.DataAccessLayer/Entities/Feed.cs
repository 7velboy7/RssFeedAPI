using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssFeedAPI.DataAccessLayer.Entities
{
    public class Feed
    {
        public Guid Id { get; set; }
        public Uri Url { get; set; } 
    }
}
