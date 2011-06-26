using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Common;
using EyeTracker.DAL;
using EyeTracker.Common.Logger;
using System.Reflection;

namespace EyeTracker.Core
{
    public class HomeService
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        HomeRepository repository;
        public HomeService()
        {
            repository = new HomeRepository();
        }

        public OperationResult Subscribe(string email)
        {
            try
            {
                log.WriteInformation("New subscribtion:{0}", email);
                repository.Subscribe(email);
                return new OperationResult();
            }
            catch(Exception exp)
            {
                return new OperationResult(exp, "Error add a new subscribtion:{0}", email);
            }
        }
    }
}
