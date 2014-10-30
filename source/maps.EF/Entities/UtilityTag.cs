using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class UtilityTag
    {
        public int Id { get; set; }


        [StringLength(50)]
        public string Name { get; set; }


        public string Color
        {
            get
            {
                switch (Id)
                {
                    case 1: return ""; //житловий фонд - сірий
                    case 2: return "alert-info"; //Транспорт - голубий
                    case 3: return "alert-danger"; //Дорожня інфраструктура - червоний
                    case 4: return "alert-success"; //Озеленення міста - зелений
                    case 5: return "alert-warning"; //Освітлення - жовтий
                    case 6: return "alert-success"; //Сміття - синій

                    default: return "";
                }
            }
        }
	}
}