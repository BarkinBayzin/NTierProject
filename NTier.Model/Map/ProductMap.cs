using NTier.Core.Map;
using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Map
{
    public class ProductMap:CoreMap<Product>
    {
        public ProductMap()
        {
            ToTable("dbo.Products");
            Property(p => p.Name).HasMaxLength(50).IsOptional();
            Property(p => p.UnitPrice).IsOptional();
            Property(p => p.Quantity).IsOptional();
            Property(p => p.UnitsInStock).IsOptional();
            Property(p => p.ImagePath).IsOptional();

            HasRequired(x => x.SubCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SubCategoryID)
                .WillCascadeOnDelete(false);
        }
    }
}
