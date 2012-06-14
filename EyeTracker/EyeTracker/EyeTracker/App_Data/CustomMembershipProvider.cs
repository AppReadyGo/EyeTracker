using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using EyeTracker.Core;
using EyeTracker.Common.Queries.Users;
using EyeTracker.Common;
using System.Collections.Specialized;


public class CustomMembershipProvider : MembershipProvider
{
    //
    // Properties from web.config, default all to False
    //
    private string applicationName;
    private bool enablePasswordReset;
    private bool enablePasswordRetrieval = false;
    private bool requiresQuestionAndAnswer = false;
    private bool requiresUniqueEmail = true;
    private int maxInvalidPasswordAttempts;
    private int passwordAttemptWindow;
    private int minRequiredPasswordLength;
    private int minRequiredNonalphanumericCharacters;
    private string passwordStrengthRegularExpression;
    private MembershipPasswordFormat passwordFormat = MembershipPasswordFormat.Hashed;

    public override string ApplicationName
    {
        get { return this.applicationName; }
        set { this.applicationName = value; }
    }

    public override void Initialize(string name, NameValueCollection config)
    {
        if (config == null)
            throw new ArgumentNullException("config");

        if (name == null || name.Length == 0)
            name = "CustomMembershipProvider";

        if (String.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "Custom Membership Provider");
        }

        base.Initialize(name, config);

        //this.applicationName = GetConfigValue(config["applicationName"],
        //              System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        //this.maxInvalidPasswordAttempts = Convert.ToInt32(
        //              GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
        //this.passwordAttemptWindow = Convert.ToInt32(
        //              GetConfigValue(config["passwordAttemptWindow"], "10"));
        //this.minRequiredNonalphanumericCharacters = Convert.ToInt32(
        //              GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
        //this.minRequiredPasswordLength = Convert.ToInt32(
        //              GetConfigValue(config["minRequiredPasswordLength"], "6"));
        //this.enablePasswordReset = Convert.ToBoolean(
        //              GetConfigValue(config["enablePasswordReset"], "true"));
        //this.passwordStrengthRegularExpression = Convert.ToString(
        //               GetConfigValue(config["passwordStrengthRegularExpression"], ""));

    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        return false;
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
        get { return this.enablePasswordReset; }
    }

    public override bool EnablePasswordRetrieval
    {
        get { return this.enablePasswordRetrieval; }
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
        get { return this.maxInvalidPasswordAttempts; }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
        get { return this.minRequiredNonalphanumericCharacters; }
    }

    public override int MinRequiredPasswordLength
    {
        get { return this.minRequiredPasswordLength; }
    }

    public override int PasswordAttemptWindow
    {
        get { return this.passwordAttemptWindow; }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
        get { return this.passwordFormat; }
    }

    public override string PasswordStrengthRegularExpression
    {
        get { return this.passwordStrengthRegularExpression; }
    }

    public override bool RequiresQuestionAndAnswer
    {
        get { return this.requiresQuestionAndAnswer; }
    }

    public override bool RequiresUniqueEmail
    {
        get { return this.requiresUniqueEmail; }
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }

    public override bool ValidateUser(string username, string password)
    {
        var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(username));
        if (securedDetails == null || securedDetails.Password != Encryption.SaltedHash(password, securedDetails.PasswordSalt))
        {
            return false;
        }
        else if (!securedDetails.Activated)
        {
            return false;
        }
        return true;
    }
}