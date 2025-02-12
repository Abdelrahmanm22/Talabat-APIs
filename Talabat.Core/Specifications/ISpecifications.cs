using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        //_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

        // Signature of property for ==> where condition [Where(P => P.Id == id)]
        public Expression<Func<T,bool>> Criteria { get; set; }


        // Signature of property for ==> List of Include [Include(P => P.ProductType).Include(P => P.ProductBrand)]
        public List<Expression<Func<T,object>>> Includes { get; set; }

    }
}
