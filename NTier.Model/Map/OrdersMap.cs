using NTier.Core.Map;
using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Map
{
    public class OrdersMap:CoreMap<Orders>
    {
        public OrdersMap()
        {
            ToTable("dbo.Orders");

            HasRequired(x => x.AppUser)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
