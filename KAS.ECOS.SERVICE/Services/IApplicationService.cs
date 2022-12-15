using KAS.ECOS.SERVICE.DTOs;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Services
{
    public interface IApplicationService
    {
        IEnumerable<ApplicationList> GetApplications();
        void AddApplication(ApplicationList application);
        bool ApplicationExist(string applicationId);
        ApplicationList GetApplicationById(string applicationId);
        Task<bool> SaveChangesAsync();
    }
}
