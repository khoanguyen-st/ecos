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

        public OrganizationService(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.OrganizationLists.Add(mapper);

                await _context.SaveChangesAsync();
                await CreateDefaultRole(mapper.Id);

                transaction.Commit();

                return mapper;
            }
            catch (Exception e)
            {
                transaction.Rollback();
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

        public async Task CreateDefaultRole(Guid organizationId)
        {
            try
            {
                var permissionList = _context.ApplicationFunctionPermissionLists.Select(p => p.Permission).ToList();

                var role = new RoleList()
                {
                    Id = new Guid(),
                    OrganizationId = organizationId,
                    RoleName = "Default",
                    IsActive = true
                };

                _context.RoleLists.Add(role);

                foreach (var permission in permissionList)
                {
                    var applicationPermission =
                                _context.ApplicationFunctionPermissionLists.FirstOrDefault(b => b.Permission == permission);

                    var roleApplicationFuntionPermissionList = new RoleApplicationFunctionPermissionList()
                    {
                        ApplicationFunctionPermissionId = applicationPermission.Id,
                        RoleId = role.Id
                    };

                    _context.RoleApplicationFunctionPermissionLists.Add(roleApplicationFuntionPermissionList);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            

        }
    }
}
