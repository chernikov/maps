using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.Model
{ 
    public partial class UtilityIssue
    {
        public enum StatusType
        {
            Create = 0x01,
            Accept = 0x02, 
            Resolved = 0x03,
            Closed = 0x04,
        }


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

        public List<UtilityTag> SubTags
        {
            get
            {
                return UtilityIssueTags.Select(p => p.UtilityTag).ToList();
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
                return UtilityIssueComments.Select(p => p.Comment).Where(p => p.ParentID == null).OrderBy(p => p.AddedDate);
            }
        }

        public UtilityIssueHistory LastHistory
        {
            get
            {
                return UtilityIssueHistories.OrderBy(p => p.HistoryDate).LastOrDefault();
            }
        }
	}
}