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
    }
}
