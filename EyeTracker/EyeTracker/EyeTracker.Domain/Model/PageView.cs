using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class PageView
    {
        public virtual long Id { get; set; }
        public virtual string Path { get; set; }
        public virtual string IP { get; set; }
        public virtual string OSLanguage { get; set; }
        public virtual string Country { get; set; }
        public virtual string City { get; set; }
        public virtual string OperationSystem { get; set; }
        public virtual string Browser { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int ScreenWidth { get; set; }
        public virtual int ScreenHeight { get; set; }
        public virtual int ClientWidth { get; set; }
        public virtual int ClientHeight { get; set; }
        public virtual Application App { get; set; }
        public virtual IList<Click> Clicks { get; set; }
        public virtual IList<ViewPart> ViewParts { get; set; }
    }
}
