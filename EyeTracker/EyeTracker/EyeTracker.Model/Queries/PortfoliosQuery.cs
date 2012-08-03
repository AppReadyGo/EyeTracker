using EyeTracker.Common.QueryResults.Portfolio;

namespace EyeTracker.Common.Queries.Analytics
{
    public class PortfoliosQuery : IQuery<PortfoliosDataResult>
    {
        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public string SearchStr { get; set; }

        public PortfoliosQuery(string searchStr, int curPage, int pageSize)
        {
            this.CurPage = curPage;
            this.PageSize = pageSize;
            this.SearchStr = searchStr;
        }
    }
}
