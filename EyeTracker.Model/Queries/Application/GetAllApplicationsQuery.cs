using EyeTracker.Common.QueryResults.Application;

namespace EyeTracker.Common.Queries.Application
{
    public class GetAllApplicationsQuery : IQuery<ApplicationsDataResult>
    {
        public int PortfolioId { get; set; }

        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public string SearchStr { get; set; }

        public GetAllApplicationsQuery(int portfolioId, string searchStr, int curPage, int pageSize)
        {
            this.PortfolioId = portfolioId;
            this.CurPage = curPage;
            this.PageSize = pageSize;
            this.SearchStr = searchStr;
        }
    }
}
