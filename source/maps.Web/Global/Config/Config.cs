using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace maps.Web.Global.Config
{
    public class Config : IConfig
    {
        public string ConnectionStrings(string connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }

        public bool DebugMode
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["DebugMode"]);
            }
        }

        public string AdminEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["AdminEmail"];
            }
        }

        public bool EnableMail
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["EnableMail"]);
            }
        }

        public IQueryable<MailTemplate> MailTemplates
        {
            get
            {
                MailTemplateConfig configInfo = (MailTemplateConfig)ConfigurationManager.GetSection("mailTemplatesConfig");
                return configInfo.mailTemplates.OfType<MailTemplate>().AsQueryable<MailTemplate>();
            }
        }
    }
}