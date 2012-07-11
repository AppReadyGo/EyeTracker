using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model.Events;

namespace EyeTracker.Domain.Model
{
    public class PageView
    {

        public PageView()
        {
            Clicks = new List<Click>();
            ViewParts = new List<ViewPart>();
            Scrolls = new List<Scroll>();
        
        }


        public virtual long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual PageView PreviousPageView { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Ip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; set; }
        /// <summary>
        /// 
        /// </summary>
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

        public virtual IList<Scroll> Scrolls { get; set; }
    }
}
