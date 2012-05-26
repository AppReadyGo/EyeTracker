using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;
using NHibernate;
using NHibernate.Linq;
using EyeTracker.Domain.Model;
using EyeTracker.Common.Interfaces;
using EyeTracker.Common.Logger;
using System.Reflection;
using System.Diagnostics.Contracts;
using EyeTracker.Common;

namespace EyeTracker.Domain.Repositories
{
    //public interface IDataRepository
    //{
    //    void ParseVisitEvent(VisitEvent visitEvent, string countryName, string city);
    //    void ParseViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvent);
    //    void ParseClickEvents(IEnumerable<ClickEvent> clickEvent);

    //    void AddPackageEvent(PackageEvent packageEvent);
    //}

    public class DataRepository : IDataRepository
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public void ParseVisitEvent(VisitEvent visitEvent, string country, string city)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                int appId = int.Parse(visitEvent.Key.Split(new char[] { '-' })[2]);
                var application = session.Get<Application>(appId);
                var visit = new PageView
                 {
                     Id = visitEvent.Id,
                     Date = visitEvent.Date,
                     Application = application,
                     Browser = null,
                     City = city,
                     ClientHeight = visitEvent.ClientHeight,
                     ClientWidth = visitEvent.ClientWidth,
                     Country = null,
                     Ip = visitEvent.Ip,
                     Language = null,
                     OperationSystem = null,
                     Path = visitEvent.Path,
                     ScreenHeight = visitEvent.ScreenHeight,
                     ScreenWidth = visitEvent.ScreenWidth,
                     PreviousPageView = null,
                 };

                if (!string.IsNullOrEmpty(visitEvent.Browser))
                {
                    visit.Browser = session.QueryOver<Browser>()
                        .Where(b => b.Name.ToLower() == visitEvent.Browser.ToLower())
                        .SingleOrDefault();
                    if (visit.Browser == null)
                    {
                        visit.Browser = new Browser { Name = visitEvent.Browser };
                    }
                }

                if (!string.IsNullOrEmpty(visitEvent.Language))
                {
                    visit.Language = session.QueryOver<Language>()
                        .Where(l => l.Name.ToLower() == visitEvent.Language.ToLower())
                        .SingleOrDefault();
                    if (visit.Language == null)
                    {
                        visit.Language = new Language { Name = visitEvent.Language };
                    }
                }

                if (!string.IsNullOrEmpty(visitEvent.OS))
                {
                    visit.OperationSystem = session.QueryOver<OperationSystem>()
                        .Where(os => os.Name.ToLower() == visitEvent.OS.ToLower())
                        .SingleOrDefault();
                    if (visit.OperationSystem == null)
                    {
                        visit.OperationSystem = new OperationSystem { Name = visitEvent.OS };
                    }
                }
                var prevPageView = visitEvent.PreviousVisitId > 0 ? session.Get<PageView>(visitEvent.PreviousVisitId) : null;
                if (!string.IsNullOrEmpty(country))
                {
                    visit.Country = session.QueryOver<Country>()
                       .Where(c => c.Name.ToLower() == country.ToLower())
                       .SingleOrDefault();
                    if (visit.Country == null)
                    {
                        visit.Country = new Country { Name = country };
                    }
                }

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(visit);
                    transaction.Commit();
                }
            }
        }

        public void ParseViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvents)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var curPart in viewPartEvents)
                    {
                        //var pageView = session.Get<PageView>(curPart.VisitInfoId);
                        //var viewPart = new ViewPart
                        //{
                        //    StartDate = curPart.StartDate,
                        //    FinishDate = curPart.FinishDate,
                        //    X = curPart.ScrollLeft,
                        //    Y = curPart.ScrollTop,
                        //    Orientation = curPart.Orientation,
                        //    PageView = pageView
                        //};
                        //session.Save(viewPart);
                        session.Save(curPart);
                    }
                    transaction.Commit();
                }
            }
        }

        public void ParseClickEvents(IEnumerable<ClickEvent> clickEvents)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var curClick in clickEvents)
                    {
                        var pageView = session.Get<PageView>(curClick.VisitInfoId);
                        var click = new Click
                        {
                            Date = curClick.Date,
                            X = curClick.ClientX,
                            Y = curClick.ClientY,
                            PageView = pageView
                        };
                        session.Save(click);
                    }
                    transaction.Commit();
                }
            }
        }

        public void AddPackageEvent(IPackageEvent packageEvent)
        {
            //Contract.Requires<ArgumentException>(packageEvent is PackageEvent);
            try
            {
                PackageEvent objPackageEvent = packageEvent as PackageEvent;
                if (objPackageEvent == null)
                {
                    log.WriteError("DataRepository::AddPackageEvent got wrong argument type");
                    return;
                }
                if(objPackageEvent.Sessions == null || objPackageEvent.Sessions.Count == 0)
                {
                    log.WriteError("DataRepository::AddPackageEvent got package with empty Sessions collection");
                    return;
                }
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    int appId = int.Parse(objPackageEvent.Key.Split(new char[] { '-' })[2]);
                    Application objApp = session.Get<Application>(appId);

                    OperationSystem objOS = session.Query<OperationSystem>().
                                            Where(os => os.Name.ToLower() == objPackageEvent.SystemInfo.RealVersionName). //check which name to use!
                                            FirstOrDefault();

                    PageView objPageView = new PageView()
                    {
                        Id = objPackageEvent.Id,
                        Application = objApp,
                        Browser = null, //todo
                        Path = objPackageEvent.Sessions[0].Path, //todo: Path should be on another level?
                        Language = null,    //todo
                        Ip = objPackageEvent.Ip,
                        Country = null, //todo: 3rd party service
                        City = null,    //same as Country
                        OperationSystem = objOS, 
                        ScreenHeight = objPackageEvent.ScreenHeight,
                        ScreenWidth = objPackageEvent.ScreenWidth,
                        ClientHeight = objPackageEvent.Sessions[0].ClientHeight,
                        ClientWidth = objPackageEvent.Sessions[0].ClientWidth
                        
                    };
                    //Clicks
                    objPageView.Clicks = new List<Click>();
                    objPackageEvent.Sessions[0].Clicks.ToList().ForEach(clickEvent =>
                        objPageView.Clicks.Add(
                        new Click()
                        {
                            PageView = objPageView,
                            X = clickEvent.ClientX,
                            Y = clickEvent.ClientY,
                            Date = clickEvent.Date
                        }));

                    //ViewParts
                    objPageView.ViewParts = new List<ViewPart>();
                    objPackageEvent.Sessions[0].ScreenViewParts.ToList().ForEach(viewPart =>
                        objPageView.ViewParts.Add(
                        new ViewPart()
                        {
                            StartDate = viewPart.StartDate,
                            FinishDate = viewPart.FinishDate,
                            X = viewPart.ScrollLeft,
                            Y = viewPart.ScrollTop,
                            Orientation = viewPart.Orientation,
                            PageView = objPageView
                        }));

                    //Scrolls
                    objPageView.Scrolls = new List<Scroll>();
                    objPackageEvent.Sessions[0].Scrolls.ToList().ForEach(scrollEvent =>
                        objPageView.Scrolls.Add(
                        new Scroll()
                        {
                            MyPageView = objPageView,
                            FirstTouch = objPageView.Clicks.FirstOrDefault(c => c.Date == scrollEvent.FirstTouch.Date),
                            LastTouch = objPageView.Clicks.FirstOrDefault(c => c.Date == scrollEvent.LastTouch.Date),
                        }));

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(objPageView);
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
