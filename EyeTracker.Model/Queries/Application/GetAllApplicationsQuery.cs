using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Common.Queries.Application
{
    public class GetAllApplicationsQuery : IQuery<ApplicationsDataResult>
    {
        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public string SearchStr { get; set; }

        public GetAllApplicationsQuery(string searchStr, int curPage, int pageSize)
        {
            this.CurPage = curPage;
            this.PageSize = pageSize;
            this.SearchStr = searchStr;
        }
    }
}
