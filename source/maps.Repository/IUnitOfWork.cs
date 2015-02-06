using maps.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Repository
{
    public interface IUnitOfWork<out TContext> : IDisposable
    {
        TContext DbContext { get; }

        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectStateable, new();

        void Save();
    }
}
