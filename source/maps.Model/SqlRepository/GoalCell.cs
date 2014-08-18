using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<GoalCell> GoalCells
        {
            get
            {
                return Db.GoalCells;
            }
        }

        private bool CreateGoalCell(GoalCell instance)
        {
            if (instance.ID == 0)
            {

                Db.GoalCells.InsertOnSubmit(instance);
                Db.GoalCells.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateGoalCell(GoalCell instance)
        {
            var cache = Db.GoalCells.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.GoalID = instance.GoalID;
				cache.Number = instance.Number;
				cache.State = instance.State;
				cache.AddedDate = instance.AddedDate;
                Db.GoalCells.Context.SubmitChanges();

                UpdateCountGoal(instance.GoalID);
                return true;
            }

            return false;
        }

        private void CreateGoalCells(int idGoal, int goalCount)
        {
            for (var i = 0; i < goalCount; i++)
            {
                CreateGoalCell(new GoalCell()
                {
                    GoalID = idGoal,
                    Number = i,
                    AddedDate = null,
                    State = 0
                });
            }
        }

        public void ClearAllCells(int idGoal)
        {
            var list = Db.GoalCells.Where(p => p.GoalID == idGoal);
            foreach (var item in list)
            {
                item.State = 0;
                item.AddedDate = null;
            }
            Db.GoalCells.Context.SubmitChanges();

            var goal = Db.Goals.FirstOrDefault(p => p.ID == idGoal);
            goal.Progress = 0;
            Db.Goals.Context.SubmitChanges();
        }
    }
}