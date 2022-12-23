using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using System.Security.Cryptography;
using System.Text;

namespace KAS.ECOS.API.Services
{
    public class EndUserService : IEndUserService
    {
        private readonly ECOSContext _context;
        private readonly IConfiguration _configuration;

        public EndUserService(ECOSContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IEnumerable<EndUserList> GetEndUsers()
        {
            return _context.EndUserLists.ToList();
        }
        public void AddEndUser(EndUserList user, Guid organizationId = default, bool isAssignOrg = false)
        {
            _context.EndUserLists.Add(user);

            if (isAssignOrg)
            {
                var organizationUser = new OrganizationUserList()
                {
                    Id = new Guid(),
                    OrganizationId = organizationId,
                    EndUserId = user.Id,
                };

                _context.OrganizationUserLists.Add(organizationUser);
            }

            _context.SaveChanges();
        }
        public bool UserEmailExist(string userEmail)
        {
            return _context.EndUserLists.Any(u => u.Email == userEmail);
        }
        public bool EndUserExist(Guid userId)
        {
            return _context.EndUserLists.Any(u => u.Id == userId);
        }

        public bool OrganizationExist(Guid organizationId)
        {
            return _context.OrganizationLists.Any(o => o.Id == organizationId);
        }
        public string HashPassword(string password)
        {
            var keySize = int.Parse(_configuration["Security:KeySize"]);
            var iterations = int.Parse(_configuration["Security:Iterations"]);
            var hashAlgorithm = HashAlgorithmName.SHA512;

            var salt = Encoding.ASCII.GetBytes(_configuration["Security:Salt"]);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
        public EndUserList? GetEndUserById(Guid userId)
        {
            return _context.EndUserLists.Where(u => u.Id == userId).FirstOrDefault();
        }
        public void DeleteEndUser(EndUserList user)
        {
            _context.EndUserLists.Remove(user);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
