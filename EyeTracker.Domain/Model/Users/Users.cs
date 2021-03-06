﻿using System;
using System.Collections.Generic;
using EyeTracker.Common;
using Iesi.Collections.Generic;
using EyeTracker.Common.Entities;


namespace EyeTracker.Domain.Model.Users
{
    public class User
    {
        private Iesi.Collections.Generic.ISet<Application> applications = null;

        public virtual int Id { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual string PasswordSalt { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual DateTime? LastAccessDate { get; protected set; }
        public virtual bool Activated { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual bool Unsubscribed { get; protected set; }
        public virtual bool SpecialAccess { get; protected set; }
        public virtual bool AcceptedTermsAndConditions { get; protected set; }
        public virtual Membership Membership { get; protected set; }
        public virtual IEnumerable<Application> Applications
        {
            get { return this.applications; }
        }

        public virtual UserType Type
        {
            get { return UserType.Member; }
        }

        protected User()
        {
            this.CreateDate = DateTime.UtcNow;
            this.Membership = Membership.Basic;
            this.PasswordSalt = Encryption.GenerateSalt();
        }

        public User(string email, string password)
            : this()
        {
            this.Email = email;
            this.Password = Encryption.SaltedHash(password, this.PasswordSalt);
        }

        public virtual void ChangePassword(string password)
        {
            this.Password = Encryption.SaltedHash(password, this.PasswordSalt);
        }

        public virtual void Activate()
        {
            this.Activated = true;
        }

        public virtual void Deactivate()
        {
            this.Activated = false;
        }

        public virtual void Unsubscribe()
        {
            this.Unsubscribed = true;
        }

        public virtual void UpdateLastAccess()
        {
            this.LastAccessDate = DateTime.UtcNow;
        }

        public virtual void AcceptTermsAndConditions(bool reset = false)
        {
            this.AcceptedTermsAndConditions = !reset;
        }

        public virtual void GrantSpecialAccess(bool specialAccess)
        {
            this.SpecialAccess = specialAccess;
        }
    }

    public class Staff : User
    {
        private Iesi.Collections.Generic.ISet<StaffRole> roles = null;

        public virtual IEnumerable<StaffRole> Roles
        {
            get { return this.roles; }
        }

        public override UserType Type
        {
            get { return UserType.Staff; }
        }

        public override Membership Membership
        {
            get { return Membership.Pro; }
        }

        public Staff(string email, string password)
            : base(email, password)
        {
        }

        protected Staff()
            : base()
        {
            this.roles = new HashedSet<StaffRole>();
        }


        public virtual void GrantRole(StaffRole role)
        {
            if (!this.roles.Contains(role))
            {
                this.roles.Add(role);
            }
        }

        public virtual void RevokeRole(StaffRole role)
        {
            if (!this.roles.Contains(role))
            {
                this.roles.Remove(role);
            }
        }
    }
}
