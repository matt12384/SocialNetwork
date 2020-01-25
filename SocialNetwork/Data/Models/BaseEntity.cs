using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
