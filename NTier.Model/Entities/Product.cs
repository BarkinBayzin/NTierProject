using NTier.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Entities
{
    public class Product:CoreEntity
    {
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public string Quantity { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public Guid SubCategoryID { get; set; }
        //Navigation Properties Begin
        public virtual SubCategory SubCategory { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
