using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BicycleDirectionLine> BicycleDirectionLines
        {
            get
            {
                return Db.BicycleDirectionLines;
            }
        }

        public bool CreateBicycleDirectionLine(BicycleDirectionLine instance)
        {
            if (instance.ID == 0)
            {
                Db.BicycleDirectionLines.InsertOnSubmit(instance);
                Db.BicycleDirectionLines.Context.SubmitChanges();
                return true;
            }

            return false;
        }
    }
}