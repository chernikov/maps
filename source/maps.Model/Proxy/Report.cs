﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class Report
    {
        public enum TypeEnum
        {
            RouteScope = 0x01,
            BusScope = 0x02,
        }

        public enum StatusEnum
        {
            New = 0x01,
            Notified = 0x02, 
            Answered = 0x03,
            Closed = 0x04,
        }


        public string StatusStr
        {
            get
            {
                switch ((StatusEnum)Status)
                {
                    case StatusEnum.New: return "Новий";
                    case StatusEnum.Notified: return "В процесі";
                    case StatusEnum.Answered: return "Відповіли";
                    case StatusEnum.Closed: return "Закритий";
                }
                return string.Empty;
            }
        }

        public IEnumerable<Rule> SubRules
        {
            get
            {
                return RuleReports.Select(p => p.Rule).ToList();
            }
        }

        public IEnumerable<ReportPhoto> SubPhotos
        {
            get
            {
                return ReportPhotos.ToList();
            }
        }

        public bool HasAccess(User user)
        {
            if (user == null) 
            {
                return false;
            }
            if (Bus != null)
            {
                return Bus.Transporteur.UserID == user.ID;
            }
            if (Route != null)
            {
                var transporteurs = Route.Bus.Select(p => p.Transporteur).Distinct().ToList();
                return transporteurs.Any(p => p.UserID == user.ID);
            }
            return false;
        }

        public bool HasAccess(Transporteur transporteur)
        {
          
            if (Bus != null)
            {
                return Bus.TransporteurID == transporteur.ID;
            }
            if (Route != null)
            {
                var transporteurs = Route.Bus.Select(p => p.Transporteur).Distinct().ToList();
                return transporteurs.Any(p => p.ID == transporteur.ID);
            }
            return false;
        }

        public bool HasAnswerFrom(Transporteur transporteur)
        {
            if (transporteur == null) 
            {
                return false;
            }
            return ReportAnswers.Any(p => p.TransporteurID == transporteur.ID);
        }
    }
}
