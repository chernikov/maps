using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Models.ViewModels
{
    public class TransporteurView
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string PrimaryPhone { get; set; }

        public string AdditionalPhone { get; set; }

    }
}