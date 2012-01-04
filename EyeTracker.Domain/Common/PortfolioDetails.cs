using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Common
{
    public class PortfolioDetails
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Visits { get; set; }

        public IEnumerable<ApplicationDetails> Applications { get; set; }
    }
}
