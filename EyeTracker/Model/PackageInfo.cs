using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Model
{
    public class PackageInfo
    {
        public List<ClickInfo> clicks { get; set; }
        public List<ViewPartInfo> parts { get; set; }
    }
}