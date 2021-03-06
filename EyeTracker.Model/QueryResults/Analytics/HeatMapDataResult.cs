﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.QueryResults.Analytics
{
    public class HeatMapDataResult
    {
        public ScreenResult Screen { get; set; }

        public IEnumerable<HeatMapItemResult> Data { get; set; }
    }
}
