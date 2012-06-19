﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics.QueryResults
{
    public class PortfolioResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<ApplicationResult> Applications { get; set; }

        public bool IsActive { get; set; }
        public long Visits { get; set; }
    }
}