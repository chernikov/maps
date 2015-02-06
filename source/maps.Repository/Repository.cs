using maps.Data;
using maps.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IObjectStateable, new()
    {
        private readonly IDbSet<TEntity> databaseSet;

        private IDbContext context;

        public Repository(IDbContext context)
        {
            this.context = context;
            this.databaseSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Entities
        {
            get
            {
                return this.databaseSet;
            }
        }

        public virtual TEntity FindById(object id)
        {
            return this.databaseSet.Find(id);
        }

        public virtual void InsertGraph(TEntity entity)
        {
            this.databaseSet.Add(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            this.databaseSet.Attach(entity);
            context.Entry(entity).State = EntityState.Added;
        }

        public virtual void Update(TEntity entity)
        {
            this.databaseSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = this.databaseSet.Find(id);
            var objectState = entity as IObjectStateable;
            if (objectState != null)
            {
                objectState.ObjState = ObjectState.Deleted;
            }

            this.Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            this.databaseSet.Attach(entity);
            this.databaseSet.Remove(entity);
        }
    }
}
