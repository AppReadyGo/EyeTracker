using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using NHibernate;
using EyeTracker.Domain;
using NHibernate.Linq;

namespace EyeTracker.DAL
{
    public interface IApplicationRepository
    {
        int Add(Application application);

        Application Get(int appId);

        void Remove(int appId);

        void Update(int appId, string description);

        IList<Application> GetAll(int portfolioId);

        long AddScreen(Screen screen);

        Screen GetScreen(int appId, int width, int height);

        EyeTrackerData GetEyeTrackerData(int appId, DateTime fromDate, DateTime toDate);
    }

    public class ApplicationRepository : IApplicationRepository
    {
        public int Add(Application application)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(application);
                    transaction.Commit();
                    return application.Id;
                }
            }
        }

        public Application Get(int appId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Application>(appId);
            }
        }

        public void Remove(int appId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var app = session.Get<Application>(appId);
                    session.Delete(app);
                    transaction.Commit();
                }
            }
        }

        public void Update(int appId, string description)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var app = session.Get<Application>(appId);
                    app.Update(description);
                    transaction.Commit();
                }
            }
        }

        public IList<Application> GetAll(int portfolioId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Application>()
                    .Where(a => a.Portfolio.Id == portfolioId).ToList();
            }
        }

        public long AddScreen(Screen screen)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(screen);
                    transaction.Commit();
                }
            }
            return screen.Id;
        }

        public Screen GetScreen(int appId, int width, int height)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Screen>()
                    .Where(s => s.ApplicationId == appId && s.Width == width && s.Height == height).FirstOrDefault();
            }
        }

        public EyeTrackerData GetEyeTrackerData(int appId, DateTime fromDate, DateTime toDate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var data = new EyeTrackerData();
                data.PageUris = session.Query<PageView>()
                    .Where(p => p.Application.Id == appId && p.Date >= fromDate && p.Date <= toDate)
                    .Select(p => p.Path)
                    .Distinct()
                    .ToList();
                data.ScreenSizes = session.Query<PageView>()
                    .Where(p => p.Application.Id == appId && p.Date >= fromDate && p.Date <= toDate)
                    .GroupBy(p => new { ClientWidth = p.ClientWidth, ClientHeight = p.ClientHeight })
                    .Select(p => new ScreenSize { Width = p.Key.ClientWidth, Height = p.Key.ClientHeight })
                    .ToList();
                return data;
            }
        }
    }
}
