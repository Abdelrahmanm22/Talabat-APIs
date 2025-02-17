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
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams Param) : 
            base(p=>
            (!Param.BrandId.HasValue || p.ProductBrandId== Param.BrandId)
            &&
            (!Param.TypeId.HasValue || p.ProductTypeId==Param.TypeId)
            &&
            (string.IsNullOrEmpty(Param.Search) || p.Name.ToLower().Contains(Param.Search))
            )
            
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
            if (!string.IsNullOrEmpty(Param.Sort))
            {
                switch (Param.Sort)
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

            #region Pagination
            //if we have 100 products 
            //and page size = 10 
            //and page index = 5

            //so=> Skip(40) and take (10)
            ApplyPagination(Param.PageSize*(Param.PageIndex-1), Param.PageSize);
            #endregion


        }

        //CTOR is Used for get product by id
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
