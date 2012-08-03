using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Common.Queries.Analytics
{
    public class ScreensQuery : IQuery<ScreensDataResult>
    {
        public int ApplicationId { get; set; }

        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public string SearchStr { get; set; }

        public bool ASC { get; set; }

        public OrderByColumn OrderBy { get; set; }

        public ScreensQuery(int applicationId, string searchStr, int curPage, int pageSize, OrderByColumn orderBy, bool asc)
        {
            this.ApplicationId = applicationId;
            this.CurPage = curPage;
            this.PageSize = pageSize;
            this.ASC = asc;
            this.OrderBy = orderBy;
            this.SearchStr = searchStr;
        }

        public enum OrderByColumn
        {
            Width,
            Height,
            Path
        }
    }
}
