using NTier.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Entities
{
    public class Orders:CoreEntity
    {
        public Guid AppUserId { get; set; }
        public bool Confirmed { get; set; }

        //Navigation Properties Begin
        public virtual List<OrderDetails> OrderDetails { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
