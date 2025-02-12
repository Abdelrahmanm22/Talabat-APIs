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
        public ProductWithBrandAndTypeSpecifications():base()
        {
            Includes.Add(P=>P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }

        //CTOR is Used for get product by id
        public ProductWithBrandAndTypeSpecifications(int id):base(P=>P.Id==id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
