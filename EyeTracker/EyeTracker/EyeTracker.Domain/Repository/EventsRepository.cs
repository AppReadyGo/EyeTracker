using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;

namespace EyeTracker.Domain.Repository
{
    public interface IEventsRepository
    {
        long AddVisitEvent(VisitEvent visitEvent);

        void AddViewPartEvent(ViewPartEvent viewPartEvent);

        void AddClickEvent(ClickEvent clickEvent);
    }

    public class EventsRepository : IEventsRepository
    {
        public long AddVisitInfo(VisitEvent visitEvent)
        {
        }

        public void AddViewPartInfo(ViewPartEvent viewPartEvent)
        {
        }

        public void AddClickInfo(ClickEvent clickEvent)
        {
        }
    }
}
