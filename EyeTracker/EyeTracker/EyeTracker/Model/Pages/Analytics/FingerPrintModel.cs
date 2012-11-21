using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyeTracker.Model.Filter;

namespace EyeTracker.Model.Pages.Analytics
{
    public class FingerPrintModel : FilterModel
    {
        public int ScreenId { get; set; }

        public string ScreenFileExtention { get; set; }

        public int PointsOnReport { get; set; }
    }
}