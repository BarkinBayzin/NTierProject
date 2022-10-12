using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTier.UI.Areas.Admin.Models
{
    public class SubCategoryVM
    {
        public List<Category> Categories{ get; set; }
        public SubCategory SubCategory { get; set; }
    }
}