﻿using maps.Instagram;
using maps.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Instagram.Controllers
{
    public abstract class InstagramController : BaseController
    {
        protected string CallbackUri = "http://" + HostName + "/instagram/oauth";

        protected string SessionIstagramName = "INSTAGRAM_INSTANCE";

        protected InsagramApiCaller InstagramApiCaller
        {
            get
            {
                if (Session["INSTAGRAM_INSTANCE"] == null)
                {
                    Session["INSTAGRAM_INSTANCE"] = new InsagramApiCaller();
                }
                return (InsagramApiCaller)Session["INSTAGRAM_INSTANCE"];
            }
        }


        public InstagramController()
        {
        }

    }
}