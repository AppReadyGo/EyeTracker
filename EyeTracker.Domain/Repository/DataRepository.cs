using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;
using NHibernate;
using EyeTracker.Domain.Model;

namespace EyeTracker.Domain.Repositories
{
    public interface IDateRepository
    {
        void ParseVisitEvent(VisitEvent visitEvent, string countryName, string city);
        void ParseViewPartEvents(IEnumerable<ViewPartEvent> viewPartEvent);
        void ParseClickEvents(IEnumerable<ClickEvent> clickEvent);
    }

    public class DataRepository : IDateRepository
    {
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
    }
}
