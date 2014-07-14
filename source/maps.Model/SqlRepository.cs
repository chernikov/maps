using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository : IRepository
    {
        [Inject]
        public mapsDbDataContext Db { get; set; }

        public IQueryable<T> GetTable<T>() where T : class
        {
            return Db.GetTable<T>().AsQueryable<T>();
        }
    }
}
