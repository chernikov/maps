﻿using maps.Social.Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace maps.Web.Global.OAuth
{
    public class FacebookAppConfig : ConfigurationSection, IFbAppConfig
    {
        [ConfigurationProperty("AppId", IsRequired = true)]
        public string AppId
        {
            get
            {
                return this["AppId"] as string;
            }

            set
            {
                this["AppId"] = value;
            }
        }

        [ConfigurationProperty("AppSecret", IsRequired = true)]
        public string AppSecret
        {
            get
            {
                return this["AppSecret"] as string;
            }

            set
            {
                this["AppSecret"] = value;
            }
        }
    }
}