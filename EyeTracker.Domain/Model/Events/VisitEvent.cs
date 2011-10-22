using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model.Events
{
    public class VisitEvent
    {
        public long UserApplicationId { get; set; }

        public string Ip { get; set; }

        public int PreviousVisitInfoId { get; set; }

        public string PageUri { get; set; }

        public int Client { get; set; }

        public int Software { get; set; }

        public int ScreenWidth { get; set; }

        public int ScreenHeight { get; set; }

        public int ClientWidth { get; set; }

        public int ClientHeight { get; set; }
    }
}
