using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.Model
{ 
    public partial class Goal
    {
        public IEnumerable<GoalCell> SubGoalCells
        {
            get
            {
                return GoalCells.OrderBy(p => p.Number).AsEnumerable();
            }
        }
	}
}