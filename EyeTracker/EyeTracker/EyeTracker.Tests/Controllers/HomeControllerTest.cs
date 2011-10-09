using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EyeTracker;
using EyeTracker.Controllers;

namespace EyeTracker.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController_ controller = new HomeController_();

            // Act
            //ViewResult result = controller.Index() as ViewResult;

            //// Assert
            //ViewDataDictionary viewData = result.ViewData;
            //Assert.AreEqual("Welcome to ASP.NET MVC!", viewData["Message"]);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController_ controller = new HomeController_();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
