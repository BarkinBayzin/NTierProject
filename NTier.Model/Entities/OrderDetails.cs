using NTier.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Entities
{
    public class OrderDetails:CoreEntity
    {
        public Guid ProductID { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        //Navigation Properties Begin
        public virtual Product Product { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
