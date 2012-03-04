using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeTracker.Model.Pages.Application
{
    public class ApplicationViewModel
    {
        public List<EyeTracker.Domain.Model.Screen> Screens { get; set; }
        public int PortfolioId { get; set; }
        public IEnumerable<SelectListItem> TypesList { get; set; }
        public string PackageLink { get; set; }
        public string PropertyId { get; set; }
        public string CodeSample { get; set; }
    }
}