using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTier.UI.Areas.Admin.Models
{
    public class UpdateProductVM
    {
        public Product Product { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}