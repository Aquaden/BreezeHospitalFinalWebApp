using Breeze.Application.Abstractions.IRepositories;
using Breeze.Persistance.MyDbContext;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.RepoImplementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Microsoft.EntityFrameworkCore.DbSet<T> Table => _dbContext.Set<T>();
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;

        }

        public async Task<bool> Add(T entity)
        {

            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> DeleteById(int id)
        {

            T data = await Table.FindAsync(id);
            return Delete(data);
        }

        public IQueryable<T> GetAll()
        {
            var query = Table.AsQueryable();
            return query;
        }

        public async Task<T> GetByid(int id)
        {
            T data = await Table.FindAsync(id);
            return data;
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public bool Delete(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }
    }
}
