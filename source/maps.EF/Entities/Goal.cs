using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public partial class Goal
    {
        public int Id { get; set; }

        public User User { get; set; }

        public bool IsPublic { get; set; }

        public string Url { get; set; }

        public string Text { get; set; }

        public int Count { get; set; }

        public int Progress { get; set; }

        public bool IsReady { get; set; }

        public int ColumnsCount { get; set; }

        public List<GoalCell> GoalCells { get; set; }

        public IEnumerable<GoalCell> SubGoalCells
        {
            get
            {
                return GoalCells.OrderBy(p => p.Number).AsEnumerable();
            }
        }
	}
}