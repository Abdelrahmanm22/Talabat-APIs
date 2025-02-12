using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        // CTOR is Used for get all Products
        public ProductWithBrandAndTypeSpecifications()
        {
            Includes.Add(P=>P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
