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
        public ProductWithBrandAndTypeSpecifications(string Sort,int? BrandId,int? TypeId) : 
            base(p=>
            (!BrandId.HasValue || p.ProductBrandId==BrandId)
            &&
            (!TypeId.HasValue || p.ProductTypeId==TypeId)
            )
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort)
                {
                    case "PriceAsc":
                        SetOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        SetOrderByDesc(P => P.Price);
                        break;
                    default:
                        SetOrderBy(P => P.Name);
                        break;
                }
            } 
        }

        //CTOR is Used for get product by id
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
