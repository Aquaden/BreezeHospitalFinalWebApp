using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Abstractions.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll();
        public Task<T> GetByid(int id);
        public Task<bool> Add(T entity);
        public bool Update(T entity);
        public Task<bool> DeleteById(int id);
        public bool Delete(T entity);
        

    }
}
