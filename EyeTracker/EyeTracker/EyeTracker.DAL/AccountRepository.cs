using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Domain;
using NHibernate;
using NHibernate.Criterion;
using EyeTracker.Common;
using Library.Common.Data;

namespace EyeTracker.DAL
{
    public interface IAccountRepository
    {
        int Add(AccountInfo account);

        AccountInfo Get(Guid UserId, int accId);

        ErrorNumber Remove(Guid UserId, int accId);

        ErrorNumber Update(AccountInfo account);
    }

    public class AccountRepository : IAccountRepository
    {
        public int Add(AccountInfo account)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(account);
                    transaction.Commit();
                }
            }
            return account.Id;
        }

        public AccountInfo Get(Guid UserId, int accId)
        {
            AccountInfo accInfo = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                accInfo = session.CreateCriteria(typeof(AccountInfo))
                    .Add(Expression.Eq("UserId", UserId))
                    .Add(Expression.Eq("Id", accId))
                    .UniqueResult<AccountInfo>();
            }
            return accInfo;
        }

        public ErrorNumber Remove(Guid UserId, int accId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var accInfo = Get(UserId, accId);
                if (accInfo == null)
                {
                    return ErrorNumber.NotFound;
                }
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(accInfo);
                    transaction.Commit();
                }
            }
            return ErrorNumber.None;
        }

        public ErrorNumber Update(AccountInfo account)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var accInfo = Get(account.UserId, account.Id);
                if (accInfo == null)
                {
                    return ErrorNumber.NotFound;
                }
                //TODO: Set all properties that is readonly for client like updated date
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Merge(account);
                    transaction.Commit();
                }
            }
            return ErrorNumber.None;
        }

    }
}
