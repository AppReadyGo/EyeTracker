using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Domain
{
    public interface ISecurityContext
    {
         Guid UserId { get; }
    }
}
