using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common.Entities;

namespace EyeTracker.Common.Commands.API
{
    public class AddPackageCommand : ICommand<int>
    {
        public long ApplicationId { get; protected set; }
        public Location Location { get; protected set; }
        public string OS { get; protected set; }
        public string Browser { get; protected set; }
        public int ScreenWidth { get; protected set; }
        public int ScreenHeight { get; protected set; }
        public SystemInfo SystemInfo { get; protected set; }
        public IEnumerable<Session> Sessions { get; protected set; }

        public AddPackageCommand(long appId, 
            Location location,
            string os, 
            string browser, 
            int screenWidth, 
            int screenHeight,
            SystemInfo systemInfo,
            IEnumerable<Session> sessions)
        {
            this.ApplicationId = appId;
            this.Location = location;
            this.OS = os;
            this.Browser = browser;
            this.ScreenHeight = screenHeight;
            this.ScreenWidth = screenWidth;
            this.SystemInfo = systemInfo;
            this.Sessions = sessions;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (this.ScreenWidth <= 0)
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "ScreenWidth must to be positive and greate than zero");
            }

            if (this.ScreenHeight <= 0)
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "ScreenHeight must to be positive and greate than zero");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }

        public class Session
        {
            public string Path { get; set; }

            public int ClientWidth { get; set; }

            public int ClientHeight { get; set; }
            /// <summary>
            /// Start this session date
            /// </summary>
            public DateTime StartDate { get; set; }
            /// <summary>
            /// Finish this session date
            /// </summary>
            public DateTime CloseDate { get; set; }
            /// <summary>
            /// Click/Touches 
            /// </summary>
            public IList<Click> Clicks { get; set; }
            /// <summary>
            /// Scrolls for this session
            /// </summary>
            public IList<Scroll> Scrolls { get; set; }
            /// <summary>
            /// Parts on the main view data 
            /// </summary>
            public IList<ViewPart> ScreenViewParts { get; set; }
        }

        public class Click
        {
            public int ClientX { get; set; }

            public int ClientY { get; set; }

            public DateTime Date { get; set; }

            public long VisitInfoId { get; set; }

            public int Press { get; set; }

            public int Orientation { get; set; }
        }

        public class Scroll
        {
            public Click FirstTouch { get; set; }
            /// <summary>
            /// Finish scrolling property
            /// </summary>
            public Click LastTouch { get; set; }
        }

        public class ViewPart
        {
            public int ScrollTop { get; set; }

            public int ScrollLeft { get; set; }

            public long TimeSpan { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime FinishDate { get; set; }

            public int Orientation { get; set; }

            //[System.Obsolete("don`t use this property", true)]
            public long VisitInfoId { get; set; }
        }
    }
}
