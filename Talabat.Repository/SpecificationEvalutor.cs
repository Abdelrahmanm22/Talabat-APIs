using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        //Function To build Query
        /// [ _dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand); ]
        
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> Spec)
        {
            var query = inputQuery; //_dbContext.Set<T>()

            if (Spec.Criteria is not null)
            {
                query = query.Where(Spec.Criteria); //dbContext.Set<T>().Where(P => P.Id == id)
            }

            if(Spec.OrderBy is not null)
            {
                query = query.OrderBy(Spec.OrderBy);
            }
            if(Spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(Spec.OrderByDesc);
            }

            query = Spec.Includes.Aggregate(query,(CurrentQuery,IncludeExpression)=> CurrentQuery.Include(IncludeExpression)); ///_dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

            return query;
        } 
    }
}
