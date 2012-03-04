using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace EyeTracker.Domain
{
    public interface IRepository
    {
        T GetById<T>(int id);

        void Add(object obj);

        ISession GetSession();
    }
}
