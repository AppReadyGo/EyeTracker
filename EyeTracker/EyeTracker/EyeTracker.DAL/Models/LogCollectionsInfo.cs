using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.DAL.Models
{
    public class LogCollectionsInfo
    {
        public Dictionary<int, string> Categories { get; set; }
        public List<string> Severities { get; set; }
    }
}
