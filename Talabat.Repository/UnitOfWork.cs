using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Contexts;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(AppDbContext dbContext)
        {
            _repositories = new Hashtable();
            this._dbContext = dbContext;
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name; //Product
            if (!_repositories.ContainsKey(type))
            {
                //First Time
                var Repo = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, Repo);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
