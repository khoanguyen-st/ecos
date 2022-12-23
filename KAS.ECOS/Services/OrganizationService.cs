using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;

namespace KAS.ECOS.SERVICE.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;
        private readonly IEndUserService _userService;

        public OrganizationService(ECOSContext context, IMapper mapper, IEndUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public List<OrganizationList> GetOrganizationLists()
        {
            return _context.OrganizationLists
                .AsNoTracking()
                .Include(o => o.RoleLists)
                .ToList();
        }

        public OrganizationList GetOrganizationById(Guid id)
        {
            return _context.OrganizationLists.Find(id);
        }

        public async Task<OrganizationList> CreateOrganizationList(OrganizationList mapper)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.OrganizationLists.Add(mapper);

                await _context.SaveChangesAsync();
                await CreateDefaultRole(mapper);
                await transaction.CommitAsync();

                return mapper;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw e;
            }
        }

        public void UpdateOrganizationList(UpdateOrganizationListDto roleList, Guid id)
        {
            try
            {
                var organization = _context.OrganizationLists.Find(id);
                _mapper.Map(roleList, organization);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteOrganizationList(Guid id)
        {
            try
            {
                var organization = _context.OrganizationLists.Find(id);
            
                _context.OrganizationLists.Remove(organization);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task CreateDefaultRole(OrganizationList organization)
        {
            try
            {
                var permissionList = _context.ApplicationFunctionPermissionLists.Select(p => p.Permission).ToList();

                var role = new RoleList()
                {
                    Id = new Guid(),
                    OrganizationId = organization.Id,
                    RoleName = "Default",
                    IsActive = true,
                    IsBaseRole = true
                };
                _context.RoleLists.Add(role);

                foreach (var roleApplicationFunctionPermissionList in permissionList
                             .Select(permission => _context.ApplicationFunctionPermissionLists.FirstOrDefault(b => b.Permission == permission))
                             .Select(applicationPermission => new RoleApplicationFunctionPermissionList()
                             {
                                 ApplicationFunctionPermissionId = applicationPermission!.Id,
                                 RoleId = role.Id
                             }))
                {
                    _context.RoleApplicationFunctionPermissionLists.Add(roleApplicationFunctionPermissionList);
                }

                var adminUser = new EndUserList()
                {
                    Id = new Guid(),
                    FirstName = organization.OrganizationName,
                    LastName = organization.OrganizationName,
                    Username = organization.OrganizationCode,
                    Email = organization.Email,
                    PhoneNumber = organization.HandPhone,
                    Password = _userService.HashPassword("admin")
                };
                _context.EndUserLists.Add(adminUser);

                var adminOrgUser = new OrganizationUserList()
                {
                    Id = new Guid(),
                    OrganizationId = organization.Id,
                    EndUserId = adminUser.Id,
                    IsAdmin = true,
                    RegistryDate = organization.RegistryDate,
                    ExpiryDate = organization.ExpiryDate
                };
                _context.OrganizationUserLists.Add(adminOrgUser);

                var adminUserRole = new EndUserRoleList()
                {
                    Id = new Guid(),
                    RoleId = role.Id,
                    OrganizationUserId = adminOrgUser.Id
                };
                _context.EndUserRoleLists.Add(adminUserRole);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
