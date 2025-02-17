using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync : BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParams Param):
            base(p =>
            (!Param.BrandId.HasValue || p.ProductBrandId == Param.BrandId)
            &&
            (!Param.TypeId.HasValue || p.ProductTypeId == Param.TypeId)
            &&
            (string.IsNullOrEmpty(Param.Search) || p.Name.ToLower().Contains(Param.Search))
            )
        {
            
        }
    }
}
