using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public virtual DateTime Date { get; set; }
        
        //[System.Obsolete("don`t use this property", true)]
        public virtual long VisitInfoId { get; set; }

        public virtual SessionInfoEvent SessionInfoEvent { get; set; }
    }
}
