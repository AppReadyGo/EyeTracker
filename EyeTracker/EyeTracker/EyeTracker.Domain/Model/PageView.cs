using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain.Model
{
    public class PageView
    {
        public virtual long Id { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Path { get; set; }

        public virtual PageView PreviousPageView { get; set; }

        public virtual string Ip { get; set; }

        public virtual Language Language { get; set; }

        public virtual Country Country { get; set; }

        public virtual string City { get; set; }

        public virtual OperationSystem OperationSystem { get; set; }

        public virtual Browser Browser { get; set; }

        public virtual int ScreenWidth { get; set; }

        public virtual int ScreenHeight { get; set; }

        public virtual int ClientWidth { get; set; }

        public virtual int ClientHeight { get; set; }

        public virtual Application Application { get; set; }

        public virtual IList<Click> Clicks { get; set; }

        public virtual IList<ViewPart> ViewParts { get; set; }
    }
}
