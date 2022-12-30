using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.SERVICE.DTOs.EndUser;
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
                var permissionList = new List<string>
                    { "3RD_MOMO_CREATE", "3RD_MOMO_UPDATE", "3RD_MOMO_CANCEL", "3RD_MOMO_CHECK", "3RD_MOMO_LIST", "ECOS_ENDUSER_CREATE", "ECOS_ROLE_CREATE", "ECOS_ROLE_UPDATE", "ECOS_ROLE_DELETE", "ECOS_GRANT_ENDUSER_ORGANIZATION_DELETE", "ECOS_GRANT_ENDUSER_ORGANIZATION_CREATE" };

                //var permissionList = _context.ApplicationFunctionPermissionLists.Select(p => p.Permission).ToList();

                var role = new RoleList()
                {
                    Id = new Guid(),
                    OrganizationId = organization.Id,
                    RoleName = "Base Role",
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

                var adminUserDto = new AddEndUserDTO()
                {
                    FirstName = organization.OrganizationName,
                    LastName = organization.OrganizationName,
                    UserName = organization.Email,
                    Email = organization.Email,
                    PhoneNumber = organization.HandPhone,
                    Password = "admin@password1"
                };

                var adminUser = _mapper.Map<EndUserList>(adminUserDto);
                var result = await _userService.AddEndUser(adminUser, adminUserDto.Password);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Something wrong! Please check your information");
                }

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
