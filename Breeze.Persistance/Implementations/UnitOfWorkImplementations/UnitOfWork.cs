using Breeze.Application.Abstractions.IRepositories;
using Breeze.Application.Abstractions.IUnitOfWorks;
using Breeze.Persistance.Implementations.RepoImplementations;
using Breeze.Persistance.MyDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.UnitOfWorkImplementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(AppDbContext myDbContext)
        {
            _dbContext = myDbContext;
            _repositories = new Dictionary<Type, object>();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }


        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            IGenericRepository<TEntity> repository = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();    
        }
    }
}
