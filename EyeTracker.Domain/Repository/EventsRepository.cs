using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;
using NHibernate;

namespace EyeTracker.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventsRepository
    {
        long AddVisitEvent(VisitEvent visitEvent);

        void AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvent);

        void AddClickEvents(IEnumerable<ClickEvent> clickEvent);
    }


    /// <summary>
    /// 
    /// </summary>
    public class EventsRepository : IEventsRepository
    {
        public long AddVisitEvent(VisitEvent visitEvent)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(visitEvent);
                    transaction.Commit();
                    return visitEvent.Id;
                }
            }
        }

        public void AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvents)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var curEvent in viewPartEvents)
                    {
                        session.Save(curEvent);
                    }
                    transaction.Commit();
                }
            }
        }

        public void AddClickEvents(IEnumerable<ClickEvent> clickEvents)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var curEvent in clickEvents)
                    {
                        session.Save(curEvent);
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
