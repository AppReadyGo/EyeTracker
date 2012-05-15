using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Interfaces;

namespace EyeTracker.Domain.Model.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewPartEvent : IEvent
    {
        //[System.Obsolete("don`t use this property", true)]
        public virtual long Id { get; set; }

        public virtual int ScrollTop { get; set; }

        public virtual int ScrollLeft { get; set; }

        public virtual long TimeSpan { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime FinishDate { get; set; }

        public virtual int Orientation { get; set; }

        //[System.Obsolete("don`t use this property", true)]
        public virtual long VisitInfoId { get; set; }

        public virtual SessionInfoEvent SessionInfoEvent { get; set; }
    }
}
