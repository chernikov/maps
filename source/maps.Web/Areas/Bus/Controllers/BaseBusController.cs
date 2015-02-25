using maps.Model;
using maps.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Areas.Bus.Controllers
{
    public class BaseBusController : BaseController
    {

        public Transporteur CurrentTransporteur
        {
            get
            {
                if (CurrentUser != null && CurrentUser.InRoles("transporteur"))
                {
                    return CurrentUser.Transporteurs.First();
                }
                return null;
            }
        }
    } 
}