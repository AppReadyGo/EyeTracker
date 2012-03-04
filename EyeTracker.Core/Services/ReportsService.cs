using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using EyeTracker.Domain;
using EyeTracker.Domain.Repositories;

namespace EyeTracker.Core.Services
{
    public interface IReportsService
    {
        OperationResult<Dictionary<DateTime, int>> GetVisitsData(DateTime from, DateTime to, int? portfolioId, int? applicationId, DataGrouping dataGrouping);
    }

    public class ReportsService : IReportsService
    {
        IReportsRepository reportRepository = null;
        public ReportsService() : this(new ReportsRepository())
        {
        }

        public ReportsService(IReportsRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        #region IReportsService Members

        public OperationResult<Dictionary<DateTime, int>> GetVisitsData(DateTime from, DateTime to, int? portfolioId, int? applicationId, DataGrouping dataGrouping)
        {
            try
            {
                return new OperationResult<Dictionary<DateTime, int>>(reportRepository.GetVisitsData(from, to, portfolioId, applicationId, dataGrouping));
            }
            catch (Exception exp)
            {
                return new OperationResult<Dictionary<DateTime, int>>();
            }
        }

        #endregion
    }
}
