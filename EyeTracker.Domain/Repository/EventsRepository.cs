using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;
using NHibernate;
using EyeTracker.Common.Logger;
using System.Reflection;

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

        void AddPackageEvent(PackageEvent packageEvent);
    }


    /// <summary>
    /// 
    /// </summary>
    public class EventsRepository : IEventsRepository
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

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


        public void AddPackageEvent(PackageEvent packageEvent)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        //////here
                        //foreach (var sessionEvent in packageEvent.Sessions)
                        //{
                        //    //AddClickEvents(sessionEvent.Clicks, false);
                        //    foreach (var clickEvent in sessionEvent.Clicks)
                        //    {
                        //        if (clickEvent.ScrollEvent != null)
                        //        {
                        //            var scrollEvent = clickEvent.ScrollEvent;
                        //            clickEvent.ScrollEvent = null;
                        //            session.SaveOrUpdate(clickEvent);
                        //            clickEvent.ScrollEvent = scrollEvent;
                        //        }

                        //    }
                        //}
                        //object objResult = 
                        session.SaveOrUpdate(packageEvent);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error saving PackageEvent");
            }
        }
    }
}
