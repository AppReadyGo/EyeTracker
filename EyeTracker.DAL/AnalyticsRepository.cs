using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Configuration;
using System.Data;
using EyeTracker.DAL.Domain;

namespace EyeTracker.DAL
{
    public interface IAnalyticsRepository
    {
        List<ClickHeatMapData> GetClickHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        List<ViewHeatMapData> GetViewHeatMapData(long appId, string pageUri, int clientWidth, int clientHeight, DateTime fromDate, DateTime toDate);

        AnalyticsInfo GetAnalyticsInfo(string userId, long? appId, string pageUri);

        void ClearAnalytics(string userId, long appId, string pageUri, int width, int height);
    }
    
    public class AnalyticsRepository : IAnalyticsRepository
    {
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
