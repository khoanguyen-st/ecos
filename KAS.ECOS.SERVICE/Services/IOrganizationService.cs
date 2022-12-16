using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.SERVICE.Services;

public interface IOrganizationService
{
    public List<OrganizationList> GetOrganizationLists();
    public OrganizationList GetOrganizationById(Guid id);
    public Task<OrganizationList> CreateOrganizationList(OrganizationList mapper);
    public void UpdateOrganizationList(UpdateOrganizationListDto roleList, Guid id);
    public void DeleteOrganizationList(Guid id);
}