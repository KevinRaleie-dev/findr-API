
using BursaryFinderAPI.Context;

namespace BursaryFinderAPI.Services;
public class OrganizationService : IOrganizationService
{
    private readonly BursaryFinderContext _ctx;
    public OrganizationService(BursaryFinderContext context)
    {
        _ctx = context;
    }
    public async Task<Organization> DeleteOrganization(int id)
    {
        var organization = _ctx.Organization.FirstOrDefault<Organization>(org => org.Id == id);
        if (organization is null)
            return null;
        
        _ctx.Organization.Remove(organization);
        await _ctx.SaveChangesAsync();

        return organization;
    }

    public Organization GetOrganization(int id)
    {
        var organization = _ctx.Organization.FirstOrDefault<Organization>(org => org.Id == id);

        if (organization is null)
            return null;
        
        return organization;
    }

    public List<Organization> GetOrganizations()
    {
        return  _ctx.Organization.ToList<Organization>();
    }
}

public interface IOrganizationService
{
    List<Organization> GetOrganizations();

    Organization GetOrganization(int id);

    Task<Organization> DeleteOrganization(int id);

}