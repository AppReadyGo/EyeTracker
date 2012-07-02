using System;
using System.Linq;
using EyeTracker.Common;
using System.Collections.Generic;
using System.Drawing;

namespace EyeTracker.Model.Pages.Analytics
{
    public class FilterParametersModel
    {
        protected int pid;

        protected int? aid { get; set; }

        protected DateTime? fd { get; set; }

        protected DateTime? td { get; set; }

        protected string ss { get; set; }

        protected string p { get; set; }

        protected string l { get; set; }

        protected string os { get; set; }

        protected string c { get; set; }

        protected string ct { get; set; }

        public int Portfolio { get { return this.pid; } }

        public int? ApplicationId { get { return this.aid; } }

        public DateTime FromDate { get { return fd.HasValue ? fd.Value.StartDay() : DateTime.UtcNow.AddDays(-30).StartDay(); } }

        public DateTime ToDate { get { return td.HasValue ? td.Value.StartDay() : DateTime.UtcNow.StartDay(); } }

        public Size? ScreenSize
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ss))
                {
                    var wh = this.ss.Split(new char[] { 'X' });
                    return new Size(int.Parse(wh[0]), int.Parse(wh[1]));
                }
                else
                {
                    return null;
                }
            }
        }

        public string Path { get { return string.IsNullOrEmpty(this.p) ? null : this.p; } }

        public string Language { get { return this.l; } }

        public string OperationSystem { get { return this.os; } }

        public string Country { get { return this.c; } }

        public string City { get { return this.ct; } }

    }
}