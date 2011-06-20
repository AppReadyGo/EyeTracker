using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.DAL
{
    internal static class Constants
    {
        public const string SP_GET_CLICK_DATA = "GetClickData";
        public const string SP_GET_VIEW_PART_DATA = "GetViewPartData";

        public const string SP_GET_ANALYTICS_INFO = "GetAnalyticsInfo";

        public const string SP_CLEAR_ANALYTICS = "ClearAnalytics";

        public const string SP_CLICK_INFO_ADD = "ClickInfo_Add";
        public const string SP_VIEW_PART_INFO_ADD = "ViewPartInfo_Add";
        public const string SP_VISIT_INFO_ADD = "VisitInfo_Add";

        public const string CLICK_INFO_VISIT_INFO_ID = "ClickInfo_VisitInfoId";
        public const string CLICK_INFO_DATE = "ClickInfo_Date";
        public const string CLICK_INFO_COUNT = "ClickInfo_Count";
        public const string CLICK_INFO_CLIENT_X = "ClickInfo_ClientX";
        public const string CLICK_INFO_CLIENT_Y = "ClickInfo_ClientY";

        public const string VIEW_PART_TIME_SPAN = "ViewPartInfo_TimeSpan";
        public const string VIEW_PART_SCROLL_LEFT = "ViewPartInfo_ScrollLeft";
        public const string VIEW_PART_SCROLL_TOP = "ViewPartInfo_ScrollTop";
        public const string VIEW_PART_INFO_VISIT_INFO_ID = "ViewPartInfo_VisitInfoId";
        public const string VIEW_PART_INFO_DATE = "ViewPartInfo_Date";
        public const string VIEW_PART_INFO_TIME_SPAN = "ViewPartInfo_TimeSpan";

        public const string VISIT_INFO_USER_APPLICATION_ID = "VisitInfo_UserApplicationId";
        public const string VISIT_INFO_PAGE_URI = "VisitInfo_PageUri";
        public const string VISIT_INFO_SCREEN_WIDTH = "VisitInfo_ScreenWidth";
        public const string VISIT_INFO_SCREEN_HEIGHT = "VisitInfo_ScreenHeight";
        public const string VISIT_INFO_CLIENT_WIDTH = "VisitInfo_ClientWidth";
        public const string VISIT_INFO_CLIENT_HEIGHT = "VisitInfo_ClientHeight";
        public const string FROM_DATE = "FromDate";
        public const string TO_DATE = "ToDate";
        public const string VISIT_INFO_ID = "VisitInfo_Id";
        public const string VISIT_INFO_SOFTWARE = "VisitInfo_Software";
        public const string VISIT_INFO_CLIENT = "VisitInfo_Client";
        public const string VISIT_INFO_PREV_VISIT_INFO_ID = "VisitInfo_PreviousVisitInfoId";
        public const string VISIT_INFO_IP = "VisitInfo_Ip";

        public const string USER_APPLICATION_USER_ID = "UserApplication_UserId";
        public const string USER_APPLICATION_ID = "UserApplication_Id";
        public const string USER_APPLICATION_NAME = "UserApplication_Name";
        public const string USER_APPLICATION_CREATE_DATE = "UserApplication_CreateDate";

        #region Logging
        #endregion Logging
    }
}
