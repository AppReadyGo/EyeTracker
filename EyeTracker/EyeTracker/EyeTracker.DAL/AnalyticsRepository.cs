using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using EyeTracker.DAL.EntityModels;
using System.Configuration;
using System.Data;
using EyeTracker.DAL.Models;

namespace EyeTracker.DAL
{
    public interface IAnalyticsRepository
    {
        List<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        List<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        long AddVisitInfo(VisitInfo visitInfo);

        void AddViewPartInfo(ViewPartInfo viewPartInfo);

        void AddClickInfo(ClickInfo clickInfo);

        AnalyticsInfo GetAnalyticsInfo(string userId, long? appId, string pageUri);

        void ClearAnalytics(string userId, long appId, string pageUri, int width, int height);
    }
    
    public class AnalyticsRepository : IAnalyticsRepository
    {
        public long AddVisitInfo(VisitInfo visitInfo)
        {
            long result = -1;
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(Constants.SP_VISIT_INFO_ADD))
            {

                database.AddInParameter(command, Constants.VISIT_INFO_USER_APPLICATION_ID, DbType.Int64, visitInfo.UserApplicationId);
                database.AddInParameter(command, Constants.VISIT_INFO_IP, DbType.String, visitInfo.Ip);
                database.AddInParameter(command, Constants.VISIT_INFO_PREV_VISIT_INFO_ID, DbType.Int64, visitInfo.PreviousVisitInfoId);
                database.AddInParameter(command, Constants.VISIT_INFO_PAGE_URI, DbType.String, visitInfo.PageUri);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT, DbType.Int32, visitInfo.Client);
                database.AddInParameter(command, Constants.VISIT_INFO_SOFTWARE, DbType.Int32, visitInfo.Software);
                database.AddInParameter(command, Constants.VISIT_INFO_SCREEN_WIDTH, DbType.Int32, visitInfo.ScreenWidth);
                database.AddInParameter(command, Constants.VISIT_INFO_SCREEN_HEIGHT, DbType.Int32, visitInfo.ScreenHeight);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_WIDTH, DbType.Int32, visitInfo.ClientWidth);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_HEIGHT, DbType.Int32, visitInfo.ClientHeight);

                database.AddOutParameter(command, Constants.VISIT_INFO_ID, DbType.Int64, 8);

                database.ExecuteNonQuery(command);

                result = (long)database.GetParameterValue(command, Constants.VISIT_INFO_ID);
            }
            return result;
            //var e = new Entities("name=EyeTrackerEntities");
            //e.AddToVisitInfos(visitInfo);
            //e.SaveChanges();
            //return e.VisitInfos.Max(curItem => curItem.Id);
        }

        public void AddViewPartInfo(ViewPartInfo viewPartInfo)
        {
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(Constants.SP_VIEW_PART_INFO_ADD))
            {
                database.AddInParameter(command, Constants.VIEW_PART_INFO_VISIT_INFO_ID, DbType.Int64, viewPartInfo.VisitInfoId);
                database.AddInParameter(command, Constants.VIEW_PART_INFO_DATE, DbType.DateTime, viewPartInfo.Date);
                database.AddInParameter(command, Constants.VIEW_PART_INFO_TIME_SPAN, DbType.Int64, viewPartInfo.TimeSpan);
                database.AddInParameter(command, Constants.VIEW_PART_SCROLL_LEFT, DbType.String, viewPartInfo.ScrollLeft);
                database.AddInParameter(command, Constants.VIEW_PART_SCROLL_TOP, DbType.Int32, viewPartInfo.ScrollTop);

                database.ExecuteNonQuery(command);
            }
            //var e = new Entities("name=EyeTrackerEntities");
            //e.AddToViewPartInfos(viewPartInfo);
            //e.SaveChanges();
        }

        public void AddClickInfo(ClickInfo clickInfo)
        {
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(Constants.SP_CLICK_INFO_ADD))
            {
                database.AddInParameter(command, Constants.CLICK_INFO_VISIT_INFO_ID, DbType.Int64, clickInfo.VisitInfoId);
                database.AddInParameter(command, Constants.CLICK_INFO_DATE, DbType.DateTime, clickInfo.Date);
                database.AddInParameter(command, Constants.CLICK_INFO_CLIENT_X, DbType.String, clickInfo.ClientX);
                database.AddInParameter(command, Constants.CLICK_INFO_CLIENT_Y, DbType.Int32, clickInfo.ClientY);

                database.ExecuteNonQuery(command);
            }
            //var e = new Entities("name=EyeTrackerEntities");
            //e.AddToClickInfos(clickInfo);
            //e.SaveChanges();
        }

        public List<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            var result = new List<ClickHeatMapData>();
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(Constants.SP_GET_CLICK_DATA))
            {
                database.AddInParameter(command, Constants.VISIT_INFO_USER_APPLICATION_ID, DbType.Int64, appId);
                database.AddInParameter(command, Constants.VISIT_INFO_PAGE_URI, DbType.String, pageUri);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_WIDTH, DbType.Int32, clientWidth);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_HEIGHT, DbType.Int32, clientHeight);
                database.AddInParameter(command, Constants.FROM_DATE, DbType.DateTime, fromDate);
                database.AddInParameter(command, Constants.TO_DATE, DbType.DateTime, toDate);
                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        result.Add(new ClickHeatMapData()
                        {
                            Count = (int)reader[Constants.CLICK_INFO_COUNT],
                            ClientX = (int)reader[Constants.CLICK_INFO_CLIENT_X],
                            ClientY = (int)reader[Constants.CLICK_INFO_CLIENT_Y]
                        });
                    }
                }
            }
            return result;
        }

        public List<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate)
        {
            var result = new List<ViewHeatMapData>();
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(Constants.SP_GET_VIEW_PART_DATA))
            {
                database.AddInParameter(command, Constants.VISIT_INFO_USER_APPLICATION_ID, DbType.Int64, appId);
                database.AddInParameter(command, Constants.VISIT_INFO_PAGE_URI, DbType.String, pageUri);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_WIDTH, DbType.Int32, clientWidth);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_HEIGHT, DbType.Int32, clientHeight);
                database.AddInParameter(command, Constants.FROM_DATE, DbType.DateTime, fromDate);
                database.AddInParameter(command, Constants.TO_DATE, DbType.DateTime, toDate);
                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        result.Add(new ViewHeatMapData()
                        {
                            TimeSpan = (int)reader[Constants.VIEW_PART_TIME_SPAN],
                            ScrollLeft = (int)reader[Constants.VIEW_PART_SCROLL_LEFT],
                            ScrollTop = (int)reader[Constants.VIEW_PART_SCROLL_TOP],
                            ScreenHeight = (int)reader[Constants.VISIT_INFO_SCREEN_HEIGHT],
                            ScreenWidth = (int)reader[Constants.VISIT_INFO_SCREEN_WIDTH]
                        });
                    }
                }
            }
            return result;
        }

        public AnalyticsInfo GetAnalyticsInfo(string userId, long? appId, string pageUri)
        {
            var result = new AnalyticsInfo();
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(Constants.SP_GET_ANALYTICS_INFO))
            {
                database.AddInParameter(command, Constants.USER_APPLICATION_USER_ID, DbType.String, userId);
                database.AddInParameter(command, Constants.USER_APPLICATION_ID, DbType.Int64, appId);
                database.AddInParameter(command, Constants.VISIT_INFO_PAGE_URI, DbType.String, pageUri);
                using (var reader = database.ExecuteReader(command))
                {
                    result.Applications = new Dictionary<long,string>();
                    while (reader.Read())
                    {
                        result.Applications.Add((long)reader[Constants.USER_APPLICATION_ID],
                            (string)reader[Constants.USER_APPLICATION_NAME]);
                    }
                    result.UriList = new List<string>();
                    reader.NextResult();
                    while (reader.Read())
                    {
                        result.UriList.Add((string)reader[Constants.VISIT_INFO_PAGE_URI]);
                    }
                    result.ClientSizes = new List<AnalyticsInfo.Size>();
                    reader.NextResult();
                    while (reader.Read())
                    {
                        result.ClientSizes.Add(new AnalyticsInfo.Size()
                        {
                            Width = (int)reader[Constants.VISIT_INFO_CLIENT_WIDTH],
                            Height = (int)reader[Constants.VISIT_INFO_CLIENT_HEIGHT],
                        });
                    }
                }
            }
            return result;
        }

        public void ClearAnalytics(string userId, long appId, string pageUri, int width, int height)
        {
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand(Constants.SP_CLEAR_ANALYTICS))
            {
                database.AddInParameter(command, Constants.USER_APPLICATION_USER_ID, DbType.String, userId);
                database.AddInParameter(command, Constants.USER_APPLICATION_ID, DbType.Int64, appId);
                database.AddInParameter(command, Constants.VISIT_INFO_PAGE_URI, DbType.String, pageUri);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_WIDTH, DbType.Int32, width);
                database.AddInParameter(command, Constants.VISIT_INFO_CLIENT_HEIGHT, DbType.Int32, height);

                database.ExecuteNonQuery(command);
            }
        }

    }
}
