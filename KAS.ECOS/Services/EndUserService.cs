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
        public void AddEndUser(EndUserList user)
        {
            _context.EndUserLists.Add(user);
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
        public string HashPassword(string password)
        {
            int keySize = Int32.Parse(_configuration["Security:KeySize"]);
            int iterations = Int32.Parse(_configuration["Security:Iterations"]);
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            byte[] salt = Encoding.ASCII.GetBytes(_configuration["Security:Salt"]);

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
