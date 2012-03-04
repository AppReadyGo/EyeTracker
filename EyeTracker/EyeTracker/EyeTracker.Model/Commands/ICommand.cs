using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Commands
{
    public interface ICommand
    {
        IEnumerable<string> Validate();
    }
}
