using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Services
{
    public interface IEndUserService
    {
        IEnumerable<EndUserList> GetEndUsers();
        void AddEndUser(EndUserList user);
        bool UserEmailExist(string userEmail);
        bool EndUserExist(Guid userId);
        string HashPassword(string password);
        EndUserList GetEndUserById(Guid userId);
        void DeleteEndUser(EndUserList user);
        Task<bool> SaveChangesAsync();
    }
}
