﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Common.Interfaces
{
    public interface IStoreRepository
    {
        void AddPackageEvent(IPackageEvent packageEvent);
    }
}