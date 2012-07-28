using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Entities;
using EyeTracker.Domain;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using EyeTracker.Domain.Model;

namespace EyeTracker.Test.Database
{
    [Binding]
    public class CreateAppsAndData
    {
        [Given(@"I have cleared the relevant tables")]
        public void GivenIHaveClearedTheRelevantTables()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var apps = session.Query<Application>().Where(app => app.Description.Contains("specflow test app "));


                //using (ITransaction t = session.BeginTransaction())
                //{

                //    apps.ForEach(app =>
                //                     {
                //                         foreach (var screen in app.Screens)
                //                         {
                //                             //screen.Application = 0;
                //                             //session.Delete(screen);
                //                             //screen = null;
                //                         }
                //                         var collection = app.Screens as ICollection<Screen>;
                //                         if (collection != null)
                //                             collection.Clear();
                //                     });
                //    t.Commit();
                //}

                using (ITransaction t = session.BeginTransaction())
                {

                    //apps.ForEach(app =>
                    //{
                    //    session.Delete(app);

                    //});
                    foreach (var application in apps)
                    {
                        foreach (var screen in application.Screens)
                        {
                            session.Delete(screen);
                        }
                        var collection = application.Screens as ICollection<Screen>;
                        if (collection != null)
                            collection.Clear();
                        session.Delete(application);
                    }

                    t.Commit();
                }

               
            }
        }


        [Given("I have created a DB connection")]
        public void GivenIhavecreatedaDBconnection()
        {
           

        }

        [Then("I have added (.*) apps")]
        public void ThenIHaveAddedNApps(int appsNumber)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using(ITransaction t = session.BeginTransaction())
                {
                    Portfolio demoPortfolio =
                        session.Query<Portfolio>().First(p => p.Description == "Demo Portfolio");
                    for (int i = 0; i < appsNumber; i++)
                    {
                        Application objApp = new Application(demoPortfolio, "specflow test app " + i, ApplicationType.Android);

                        session.Save(objApp);
                        Assert.AreNotEqual(0, objApp.Id);
                    }
                    t.Commit();
                }
            }
        }

        [Then("I have created (.*) touches for each page view")]
        public void ThenICHaveCreatedNTouches(int touchesNumberPerApp)
        {
            Random random = new Random();
            
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction t = session.BeginTransaction())
                {
                    var apps = session.Query<Application>().Where(app => app.Description.Contains("specflow test app "));
                    foreach(var app in apps)
                    {
                        var pageView = session.Query<PageView>().First(pv => pv.Application.Id == app.Id);
                        for (int i = 0; i < touchesNumberPerApp; i++)
                        {
                            Click click = new Click();
                            click.Date = DateTime.UtcNow;
                            click.Orientation = random.Next(0, 1);
                            click.PageView = pageView;
                            click.X = random.Next(0, pageView.ClientWidth);
                            click.Y = random.Next(0, pageView.ScreenHeight);

                            pageView.Clicks.Add(click);
                        }
                        session.Save(pageView);
                    }
                    t.Commit();
                }
            }
        }

        [Then(@"I have added a screen for each app with height=(.*) and width=(.*)")]
        public void ThenIHaveAddedAScreenForEachApp(int screenHeight, int screenWidth)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction t = session.BeginTransaction())
                {
                    var apps = session.Query<Application>().Where(app => app.Description.Contains("specflow test app "));
                    foreach (var app in apps)
                    {
                        Screen screen = new Screen();
                        //screen.ApplicationId = app.Id;
                        screen.Application = app;
                        screen.Height = screenHeight;
                        screen.Width = screenWidth;
                        screen.FileExtension = "jpg";
                       
                        session.Save(screen);
                    }
                    t.Commit();
                }
            }
        }


        [Then(@"I have added a page view for each app with client height=(.*) and client width=(.*)")]
        public void ThenIHaveAddedAPageViewForEachApp(int clientHeight, int clientWidth )
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction t = session.BeginTransaction())
                {
                    var apps = session.Query<Application>().Where(app => app.Description.Contains("specflow test app "));
                    foreach (var app in apps)
                    {
                        PageView pageView = new PageView();
                        pageView.Application = app;
                        pageView.ClientHeight = clientHeight;
                        pageView.ClientWidth = clientWidth;
                        pageView.Date = DateTime.UtcNow;
                        pageView.Path = "pageView for app " + app.Id;
                        pageView.ScreenHeight = app.Screens.First().Height;
                        pageView.ScreenWidth = app.Screens.First().Width;
                        
                        session.Save(pageView);
                    }
                    t.Commit();
                }
            }
        }

    }
}
