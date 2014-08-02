using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{

    public partial class SqlRepository
    {
        public IQueryable<BicycleLine> BicycleLines
        {
            get
            {
                return Db.BicycleLines;
            }
        }

        public bool ClearAllBicycleLines()
        {
            var list = Db.BicycleLines.ToList();
            Db.BicycleLines.DeleteAllOnSubmit(list);
            Db.BicycleLines.Context.SubmitChanges();
            return true;
        }


        public bool CreateBicycleLine(BicycleLine instance)
        {
            if (instance.ID == 0)
            {
                Db.BicycleLines.InsertOnSubmit(instance);
                Db.BicycleLines.Context.SubmitChanges();
                return true;
            }

            return false;
        }
    }
}