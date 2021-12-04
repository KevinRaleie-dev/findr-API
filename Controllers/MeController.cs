using BursaryFinderAPI.Services;
using BursaryFinderAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using BursaryFinderAPI.Models;

[ApiController]
[Route("/api/me/")]
public class MeController : ControllerBase
{
    private IMe _me;
    public MeController(IMe me)
    {
        _me = me;
    }

    [HttpPost("user")]
    public ActionResult<User> MeUser(MeDto dto)
    {
        try
        {
            var user = _me.GetCurrentUser(dto);

            if (user is null)
                return BadRequest("Token unverifiable");

            return Ok(new
            {
                id = user.Id,
                firstName = user.FirstName,
                lastName = user.LastName,
                fullName = user.FullName,
                email = user.Email,
                bio = user.Bio,
                PhoneNumber = user.PhoneNumber,
            });
        }
        catch (System.Exception ex)
        {
            // TODO
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }

    [HttpPost("organization")]
    public ActionResult<Organization> MeOrganization(MeDto dto)
    {
        try
        {

            var organization = _me.GetCurrentOrganization(dto);

            if (organization is null)
                return NotFound();

            return Ok(organization);

        }
        catch (System.Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }
}