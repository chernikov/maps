using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool;

namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Goal> Goals
        {
            get
            {
                return Db.Goals;
            }
        }

        public bool CreateGoal(Goal instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.Count = 100;
                instance.ColumnsCount = 10;
                Db.Goals.InsertOnSubmit(instance);
                Db.Goals.Context.SubmitChanges();

                CreateGoalCells(instance.ID, instance.Count);
                return true;
            }
            return false;
        }

        public bool UpdateGoal(Goal instance)
        {
            var cache = Db.Goals.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.IsPublic = instance.IsPublic;
				cache.Url = instance.Url;
                cache.ColumnsCount = instance.ColumnsCount;
				cache.Text = instance.Text;
				cache.Count = instance.Count;
				cache.Progress = instance.Progress;
				cache.IsReady = instance.IsReady;
                Db.Goals.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        
        public bool RemoveGoal(int idGoal)
        {
            Goal instance = Db.Goals.FirstOrDefault(p => p.ID == idGoal);
            if (instance != null)
            {
                var list = Db.GoalCells.Where(p => p.GoalID == idGoal).ToList();
                Db.GoalCells.DeleteAllOnSubmit(list);
                Db.Goals.DeleteOnSubmit(instance);
                Db.Goals.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        private void UpdateCountGoal(int idGoal)
        {
            Goal instance = Db.Goals.FirstOrDefault(p => p.ID == idGoal);
            if (instance != null)
            {
                instance.Progress = instance.GoalCells.Count(p => p.State == 1);
                Db.Goals.Context.SubmitChanges();
            }
        }
    }
}