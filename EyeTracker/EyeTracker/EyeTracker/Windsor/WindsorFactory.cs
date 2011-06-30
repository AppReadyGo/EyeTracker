using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections;

namespace EyeTracker.Windsor
{
    public class WindsorFactory : DefaultControllerFactory
    {
        private static IWindsorContainer windsorContainer;

        public WindsorFactory(IWindsorContainer container)
        {
            windsorContainer = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return windsorContainer.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;
            if (disposableController != null)
            {
                disposableController.Dispose();
            }

            windsorContainer.Release(controller);
        }

        public static T Resolve<T>()
        {
            if (windsorContainer != null)
                return windsorContainer.Resolve<T>();
            else
                return default(T);
        }

        public static T Resolve<T>(IDictionary arguments)
        {
            if (windsorContainer != null)
                return windsorContainer.Resolve<T>(arguments);
            else
                return default(T);
        }
        public static T Resolve<T>(object argumentsAsAnonymousType)
        {
            if (windsorContainer != null)
                return windsorContainer.Resolve<T>(argumentsAsAnonymousType);
            else
                return default(T);
        }

        public static T Resolve<T>(string key)
        {
            if (windsorContainer != null)
                return windsorContainer.Resolve<T>(key);
            else
                return default(T);
        }

    }
}