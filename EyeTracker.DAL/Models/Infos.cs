using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EyeTracker.DAL.Models
{
    [DataContract]
    public class VisitInfo
    {
        [DataMember(Name = "cid")]
        public string ClientId { get; set; }

        [DataMember(Name = "sw")]
        public int ScreenWidth { get; set; }

        [DataMember(Name = "sh")]
        public int ScreenHeight { get; set; }

        [DataMember(Name = "cw")]
        public int ClientWidth { get; set; }

        [DataMember(Name = "ch")]
        public int ClientHeight { get; set; }

        public string Uri { get; set; }

    }

    [DataContract]
    public class Package
    {
        [DataMember(Name = "cid")]
        public string ClientId { get; set; }

        [DataMember(Name = "vpd")]
        public ViewPartInfo[] ViewParts { get; set; }

        [DataMember(Name = "cd")]
        public ClickInfo[] Clicks { get; set; }

        public string Uri { get; set; }
    }

    [DataContract]
    public class ClickInfo
    {
        [DataMember(Name = "d")]
        public string StrDate { get; set; }

        public DateTime Date { get { return DateTime.Parse(StrDate); } }

        [DataMember(Name = "cx")]
        public int ClientX { get; set; }

        [DataMember(Name = "cy")]
        public int ClientY { get; set; }

        public int Count { get; set; }
    }

    [DataContract]
    public class ViewPartInfo
    {
        [DataMember(Name = "sd")]
        public string StrStartDate { get; set; }

        public DateTime StartDate { get { return DateTime.Parse(StrStartDate); } }

        [DataMember(Name = "sl")]
        public int ScrollLeft { get; set; }

        [DataMember(Name = "st")]
        public int ScrollTop { get; set; }

        [DataMember(Name = "fd")]
        public string StrFinishDate { get; set; }

        public DateTime FinishDate { get { return DateTime.Parse(StrFinishDate); } }

        public int TimeSpan { get { return (FinishDate - StartDate).Seconds; } }
    }

}
