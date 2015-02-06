using maps.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IObjectStateable, new()
    {
        TEntity FindById(object id);

        void InsertGraph(TEntity entity);

        void Update(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);

        void Insert(TEntity entity);

        IQueryable<TEntity> Entities { get; }

    }
}
