using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Contexts;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            ///h3ml el moskn
            if (typeof(T)==typeof(Product))
                return (IEnumerable<T>) await dbContext.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType).ToListAsync();
            else
                return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //return await dbContext.Set<T>().Where(p=>p.Id == id).FirstOrDefaultAsync();
            return await dbContext.Set<T>().FindAsync(id); //to search local
        }
    }
}
