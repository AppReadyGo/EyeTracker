using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Core.Commands
{
    public interface ICommand
    {
        IEnumerable<string> Validate();
    }
}
