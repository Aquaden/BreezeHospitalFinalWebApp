using Breeze.Application.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        public Task<int> SaveAsync();
    }
}
