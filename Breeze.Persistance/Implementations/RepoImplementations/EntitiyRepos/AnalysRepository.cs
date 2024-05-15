using Breeze.Application.Abstractions.IRepositories.IEntityRepos;
using Breeze.Domain.Entities;
using Breeze.Persistance.MyDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Implementations.RepoImplementations.EntitiyRepos
{
    public class AnalysRepository : GenericRepository<Analyses>, IAnalysRepository
    {
        public AnalysRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
