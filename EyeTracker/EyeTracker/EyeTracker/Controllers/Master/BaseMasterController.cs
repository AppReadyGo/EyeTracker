using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Model;

namespace EyeTracker.Controllers.Master
{
    public abstract class BaseMasterController<TMasterViewModel> : Controller
        where TMasterViewModel : class, new()
    {
        /// <summary>
        /// Views this instance.
        /// </summary>
        /// <returns></returns>
        protected virtual new ActionResult View()
        {
            return View(ViewData.Model);
        }

        /// <summary>
        /// Views the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected virtual new ActionResult View(object model)
        {
            var masterModel = GetMasterViewModel();
            object wrapper = CreateModel(model, masterModel);

            return base.View(wrapper);
        }

        /// <summary>
        /// Views the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected virtual new ActionResult View(string viewName, object model)
        {
            var masterModel = GetMasterViewModel();
            object wrapper = CreateModel(model, masterModel);

            return base.View(viewName, wrapper);
        }

        /// <summary>
        /// Gets the master view model.
        /// override this in your master controller.
        /// </summary>
        /// <returns></returns>
        protected abstract TMasterViewModel GetMasterViewModel();

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="masterModel">The master model.</param>
        /// <returns></returns>
        protected virtual object CreateModel(object model, TMasterViewModel masterModel)
        {
            var modelType = typeof(object);

            if (model != null)
                modelType = model.GetType();

            var types = new[] { typeof(TMasterViewModel), modelType };
            Type generic = typeof(ViewModelWrapper<,>).MakeGenericType(types);

            return Activator.CreateInstance(generic, masterModel, model);
        }
    }
}