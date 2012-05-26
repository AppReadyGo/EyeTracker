using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Common.Interfaces;
using System.Diagnostics.Contracts;
using EyeTracker.Domain.Model;

namespace EyeTracker.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventsRepository : IStoreRepository
    {
        long AddVisitEvent(VisitEvent visitEvent);

        void AddViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvent);

        void AddClickEvents(IEnumerable<ClickEvent> clickEvent);

        //void AddPackageEvent(PackageEvent packageEvent);
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


        public void AddPackageEvent(IPackageEvent packageEvent)
        {
            //Contract.Requires<ArgumentException>(packageEvent is PackageEvent);
            PackageEvent objPackageEvent = packageEvent as PackageEvent;
            if (objPackageEvent == null)
            {
                log.WriteError(new ArgumentException("EventsRepository::AddPackageEvent got wrong argument"), "packageEvent");
                return;
            }
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    int appId = int.Parse(objPackageEvent.Key.Split(new char[] { '-' })[2]);
                    Application objApp = session.Get<Application>(appId);
#if DEBUG
                    if (objApp == null)
                    {

                        //Portfolio port = new Portfolio();
                        //port.Update("gogo", 0);
                        //objApp = new Application(port, "debug app", ApplicationType.Android);

                        //session.SaveOrUpdate(port);
                    }
#endif
                    objPackageEvent.Application = objApp;

                    //var test = session.Get<OperationSystem>(1);
                    //var test2 = session.Query<OperationSystem>().ToList();
                    OperationSystem objOS = session.Query<OperationSystem>().
                                            Where(os => os.Name.ToLower() == objPackageEvent.SystemInfo.RealVersionName). //check which name to use!
                                            FirstOrDefault();
                    objPackageEvent.OperationSystem = objOS;

                    //todo: browser
                    //todo: language
                    //todo: country
                    //todo: city

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
                        session.SaveOrUpdate(objPackageEvent);
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
