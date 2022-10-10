using NTier.Core.Entity.Enum;
using NTier.Model.Entities;
using NTier.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Service.Option
{
    public class OrderService:BaseService<Orders>
    {
        public List<Orders> ListPendingOrders()
        {
            return GetDefaults(x => x.Confirmed == false &&
            x.Status == Status.Active).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public int PendingOrderCount()
        {
            return GetDefaults(x => x.Confirmed == false &&
            x.Status == Status.Active).Count;
        }
    }
}
