using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace EyeTracker.Domain
{
    public class Repository : IRepository
    {
        #region IRepository Members

        public T GetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(object obj)
        {
            throw new NotImplementedException();
        }

        public ISession GetSession()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
