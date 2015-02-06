using maps.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Repository
{
    public interface IUnitOfWorkFactory<TContext> where TContext : IDbContext
    {
        IUnitOfWork<TContext> Create();
    }
}
