using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTier.UI.Areas.Member.Models.VM
{
    public class ProductVM
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}