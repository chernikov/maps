using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Models.Info
{
    public class MessageInfo
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public MessageInfo(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }

        public MessageInfo(string message)
        {
            this.Message = message;
        }
    }
}