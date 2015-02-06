using maps.Data;
using maps.Data.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Repository
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : IDbContext, new()
    {
        private readonly TContext context;

        private bool disposed;

        private Hashtable repositories;

        public UnitOfWork(TContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            this.context = new TContext();
        }

        public TContext DbContext
        {
            get
            {
                return this.context;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {

            if (!this.disposed)
            {
                if (disposing)
                {
                    Save();
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> Repository<T>() where T : class, IObjectStateable, new()
        {
            if (this.repositories == null)
            {
                this.repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this.context);

                this.repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
