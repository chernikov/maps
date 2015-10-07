using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.Model
{ 
    public partial class UtilityTag
    {
        public string Color
        {
            get
            {
                switch (ID)
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