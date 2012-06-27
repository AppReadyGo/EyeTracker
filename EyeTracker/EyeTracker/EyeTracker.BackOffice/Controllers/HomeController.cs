using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeTracker.Core;
using AutoMapper;
using EyeTracker.Common;
using EyeTracker.DAL.Domain;
using System.Text;
using EyeTracker.Common.Logger;
using System.Reflection;
using EyeTracker.Core.Services;

namespace EyeTracker.BackOffice.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        
        //IBackOfficeService service;
        //public HomeController()
        //    : this(new BackOfficeService())
        //{
        //}

        //public HomeController(IBackOfficeService service)
        //{
        //    this.service = service;
        //}

        public ActionResult Index(string searchStr, int? categoriesList, string severityList, DateTime? fromDate, DateTime? toDate, int? processId, int? threadId)
        {
            GetLogsData(searchStr, categoriesList, severityList, fromDate, toDate, processId, threadId);

            return View();
        }

        private void GetLogsData(string searchStr, int? categoriesList, string severityList, DateTime? fromDate, DateTime? toDate, int? processId, int? threadId)
        {
            if (severityList == "All")
            {
                severityList = null;
            }
            if (categoriesList.HasValue && categoriesList.Value == -1)
            {
                categoriesList = null;
            }
            OperationResult<List<LogInfo>> logInfoList = service.GetLogs(
                searchStr,
                categoriesList,
                severityList,
                fromDate,
                toDate,
                processId,
                threadId
                );
            StringBuilder sb = new StringBuilder();
            if (!logInfoList.HasError)
            {
                foreach (var info in logInfoList.Value)
                {
                    sb.AppendLine(string.Format("{2} | ProcessId:{0}, ThreadId:{1}", info.ProcessID, info.Win32ThreadId, info.FormattedMessage));
                }
            }
            else
            {
                ViewData["errorMessage"] = logInfoList.ErrorMessage;
            }



            List<SelectListItem> categoriesListItems = null, severitiesListItems = null;
            var logCollRes = service.GetLogCollections();
            if (!logCollRes.HasError)
            {
                Mapper.CreateMap<KeyValuePair<int, string>, SelectListItem>()
                   .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Value))
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Key));
                categoriesListItems = Mapper.Map<Dictionary<int, string>, List<SelectListItem>>(logCollRes.Value.Categories);
                Mapper.CreateMap<string, SelectListItem>()
                   .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src))
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src));
                severitiesListItems = Mapper.Map<List<string>, List<SelectListItem>>(logCollRes.Value.Severities);
            }
            else
            {
                ViewData["errorMessage"] = logCollRes.ErrorMessage;
            }
            ViewData["searchStr"] = searchStr;
            ViewData["categoriesList"] = categoriesListItems ?? new List<SelectListItem>();
            ViewData["severityList"] = severitiesListItems ?? new List<SelectListItem>();
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            ViewData["processId"] = processId;
            ViewData["threadId"] = threadId;
            ViewData["errorMessage"] = string.Empty;
            ViewData["output"] = sb.ToString();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Clear()
        {

            var res = service.ClearLog();
            return RedirectToAction("Index");
        }
    }
}
