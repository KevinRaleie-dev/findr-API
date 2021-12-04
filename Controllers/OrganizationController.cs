
using BursaryFinderAPI.Context;
using BursaryFinderAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/")]
public class OrganizationController : ControllerBase
{

    private IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet("organizations")]
    public List<Organization> GetOrganizations() => _organizationService.GetOrganizations();

    [HttpGet("organizations/{id}")]
    public ActionResult<Organization> GetOrganization(int id)
    {
        var organization = _organizationService.GetOrganization(id);

        if (organization is null)
            return StatusCode(404);
        
        return Ok(organization);
    
    }

    [HttpDelete("organizations/{id}")]
    public async Task<ActionResult> DeleteOrganization(int id)
    {
        var entity = await _organizationService.DeleteOrganization(id);
        if (entity is null)
            return StatusCode(404);
        
        return Ok();
        
    }

}