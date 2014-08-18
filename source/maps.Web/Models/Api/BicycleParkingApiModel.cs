using System.Web;

namespace maps.Web.Models.Api
{
    public class BicycleParkingApiModel
    {
        public int ID { get; set; }

        public int Type { get; set; }

        public string PhotoUrl { get; set; }

        public string Position
        {
            get { return string.Format("({0},{1})", Lat, Lng); }
        }

        public double Lat { get; set; }
        
        public double Lng { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public int? Capacity { get; set; }

       /* public HttpPostedFileBase File { get; set; }*/
    }
}