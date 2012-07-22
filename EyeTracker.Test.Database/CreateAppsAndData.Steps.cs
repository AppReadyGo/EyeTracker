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
                using (ITransaction t = session.BeginTransaction())
                {
                    var apps = session.Query<Application>().Where(app => app.Description.Contains("specflow test app "));
                    apps.ForEach(app => session.Delete(app));
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

        [Then("I have created (.*) touches for each app")]
        public void ThenICHaveCreatedNTouches(int touchesNumberPerApp)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction t = session.BeginTransaction())
                {
                    var apps = session.Query<Application>().Where(app => app.Description.Contains("specflow test app "));
                    foreach(var app in apps)
                    {
                        //todo: add scrolls
                    }
                }
            }
        }
    }
}
