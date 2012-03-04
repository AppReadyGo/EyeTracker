using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeTracker.Domain.Model;
using EyeTracker.Domain.Repositories;
using EyeTracker.Domain.Model.BackOffice;

namespace EyeTracker.Core.Services
{
    public interface IAdminService
    {
        MembershipInfo GetMembership();

        T Get<T>(Guid id) where T : Entity, new();

        IList<T> GetAll<T>() where T : Entity, new();

        void Edit<T>(T entity) where T : Entity, new();
    }

    public class AdminService : IAdminService
    {
        IAdminRepository adminRepository = null;
        public AdminService()
            : this(new AdminRepository())
        {
        }
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public MembershipInfo GetMembership()
        {
            return adminRepository.GetMembership();
        }

        public T Get<T>(Guid id) where T : Entity, new()
        {
            return adminRepository.Get<T>(id);
        }

        public IList<T> GetAll<T>() where T : Entity, new()
        {
            return adminRepository.GetAll<T>();
        }

        public void Edit<T>(T entity) where T : Entity, new()
        {
            adminRepository.Edit<T>(entity);
        }
    }
}
