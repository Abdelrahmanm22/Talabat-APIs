using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync (T item);
        void Update (T item);
        void Delete (T item);

        ////////////////With Specification//////////////////////
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec);
        Task<int> GetCountWithSpecAsync(ISpecifications<T> Spec);
        ////////////////With Specification//////////////////////



    }
}
