using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class UtilityIssue
    {
        public enum StatusType
        {
            Create = 0x01,
            Accept = 0x02, 
            Resolved = 0x03,
            Closed = 0x04,
        }

        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public City City { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? AcceptedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public UtilityDepartment UtilityDepartment { get; set; }

        public UtilityIssue MainUtilityIssue { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Solution { get; set; }

        public int Status { get; set; }

        public bool IsRemoved { get; set; }

        public List<UtilityPhoto> UtilityPhotos { get; set; }

        public List<Comment> Comments { get; set; }

        public List<UtilityTag> UtilityTags { get; set; }

        public string StatusText
        {
            get
            {
                switch ((StatusType)Status)
                {
                    case StatusType.Create: return "Новий";
                    case StatusType.Accept: return "Прийнятий";
                    case StatusType.Resolved: return "Вирішений";
                    case StatusType.Closed: return "Закритий";
                }
                return "Невідомий";
            }
        }

        public string StatusClass
        {
            get
            {
                switch ((StatusType)Status)
                {
                    case StatusType.Create: return "label-danger";
                    case StatusType.Accept: return "label-primary";
                    case StatusType.Resolved: return "label-success";
                    case StatusType.Closed: return "label-warnings";
                }
                return "label-info";
            }
        }

        public string StatusIcon
        {
            get
            {
                switch ((StatusType)Status)
                {
                    case StatusType.Create: return "danger-icon.png";
                    case StatusType.Accept: return "primary-icon.png";
                    case StatusType.Resolved: return "success-icon.png";
                    case StatusType.Closed: return "warnings-icon.png";
                }
                return "info-icon.png";
            }
        }

        public IEnumerable<UtilityPhoto> SubPhotos
        {
            get
            {
                return UtilityPhotos.Where(p => !p.IsRemoved).AsEnumerable();
            }
        }


        public UtilityPhoto FirstImage
        {
            get
            {
                return UtilityPhotos.FirstOrDefault(p => !p.IsRemoved);
            }
        }

        public IEnumerable<Comment> SubComments
        {
            get
            {
                return Comments.Where(p => p.Parent == null).OrderBy(p => p.AddedDate);
            }
        }
	}
}