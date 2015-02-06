using maps.Data;
using maps.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Core
{
    public abstract class BaseManager
    {
        protected readonly IUnitOfWorkFactory<IMapsContext> _unitOfWorkFactory;

        public BaseManager(IUnitOfWorkFactory<IMapsContext> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
    }
}
