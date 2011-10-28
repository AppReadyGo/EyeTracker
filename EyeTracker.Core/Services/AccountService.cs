using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.DAL.Domain;
using EyeTracker.Common;
using EyeTracker.DAL;
using System.Security;
using System.Net;
using System.Collections.ObjectModel;

namespace EyeTracker.Core.Services
{
    public interface IAccountService
    {
        OperationResult<int> Add(AccountInfo account);
        OperationResult<AccountInfo> Get(int accId);
        OperationResult Remove(int accId);
        OperationResult Update(AccountInfo account);
    }

    public class AccountService : IAccountService
    {
        IAccountRepository repository = null;
        private IMembershipService membershipService = null;
        public AccountService()
            : this(new AccountMembershipService(), new AccountRepository())
        {
        }

        public AccountService(IMembershipService membershipService, IAccountRepository repository)
        {
            this.membershipService = membershipService;
            this.repository = repository;
        }

        public OperationResult<int> Add(AccountInfo account)
        {
            try
            {
                account.FirstName = WebUtility.HtmlDecode(account.FirstName);
                account.LastName = WebUtility.HtmlDecode(account.LastName);
                //Check Security
                var userRes = membershipService.GetCurrentUserId();
                if (userRes.HasError)
                {
                    return new OperationResult<int>(userRes);
                }
                account.UserId = userRes.Value;
                //Check account properties
                ErrorNumber res = CheckAccountProperties(account);
                if (res != ErrorNumber.None)
                {
                    return new OperationResult<int>(res);
                }
                return new OperationResult<int>(repository.Add(account));
            }
            catch (Exception exp)
            {
                return new OperationResult<int>(exp);
            }
        }

        private ErrorNumber CheckAccountProperties(AccountInfo account)
        {
            //Check account properties
            if (account.FirstName.Length > 50)
            {
                return ErrorNumber.WrongFirstName;
            }
            if (account.LastName.Length > 50)
            {
                return ErrorNumber.WrongLastName;
            }
            if (account.TimeZone < -12 || account.TimeZone > 14)
            {
                return ErrorNumber.WrongTimeZone;
            }
            return ErrorNumber.None;
        }

        public OperationResult<AccountInfo> Get(int accId)
        {
            try
            {
                //Check Security
                var userRes = membershipService.GetCurrentUserId();
                if (userRes.HasError)
                {
                    return new OperationResult<AccountInfo>(userRes);
                }
                var accInfo = repository.Get(userRes.Value, accId);
                if (accInfo == null)
                {
                    return new OperationResult<AccountInfo>(ErrorNumber.NotFound);
                }
                return new OperationResult<AccountInfo>(accInfo);
            }
            catch (Exception exp)
            {
                return new OperationResult<AccountInfo>(exp);
            }
        }

        public OperationResult Remove(int accId)
        {
            //Check Security
            try
            {
                //Check Security
                var userRes = membershipService.GetCurrentUserId();
                if (userRes.HasError)
                {
                    return new OperationResult<AccountInfo>(userRes);
                }
                return new OperationResult(repository.Remove(userRes.Value, accId));
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }

        public OperationResult Update(AccountInfo account)
        {
            try
            {
                //Check Security
                var userRes = membershipService.GetCurrentUserId();
                if (userRes.HasError)
                {
                    return new OperationResult<AccountInfo>(userRes);
                }
                account.UserId = userRes.Value;
                //Check account properties
                ErrorNumber res = CheckAccountProperties(account);
                if (res != ErrorNumber.None)
                {
                    return new OperationResult(res);
                }                
                return new OperationResult(repository.Update(account));
            }
            catch (Exception exp)
            {
                return new OperationResult(exp);
            }
        }
    }
}
