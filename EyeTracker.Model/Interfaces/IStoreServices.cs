using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Interfaces
{
    public interface IStoreServices
    {
        OperationResult HandlePackageEvent(IPackageEvent packageEvent);
    }
}
