using BursaryFinderAPI.Dtos;
using BursaryFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BursaryFinderAPI.Services;

[ApiController]
[Route("/api/auth/")]
public class AuthController : ControllerBase
{
    private IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register/user")]
    public async Task<ActionResult> RegisterUser(User userIn)
    {
        var user = await _auth.RegisterUser(userIn);

        if (user is null)
            return BadRequest(new
            {
                message = "Account already registered",
            });

        return Ok(new
        {
            message = "Account created successfully"
        });
    }

    [HttpPost("register/organization")]
    public async Task<ActionResult> RegisterOrganization(Organization org)
    {

        var organization = await _auth.RegisterOrganization(org);

        if (organization is null)
            return BadRequest(new
            {
                message = "Account already registered"
            });

        return Ok(new
        {
            message = "Account successfully created"
        });

    }

    [HttpPost("login/user")]
    public ActionResult LoginUser(LoginDto dto)
    {
        var token = _auth.LoginUser(dto);

        if (string.IsNullOrEmpty(token))
            return BadRequest(new
            {
                message = "Invalid Email or Password"
            });

        return Ok(new
        {
            token = token
        });
    }

    [HttpPost("login/organization")]
    public ActionResult LoginOrganization(LoginDto dto)
    {
        var token = _auth.LoginOrganization(dto);

        if (string.IsNullOrEmpty(token))
            return BadRequest(new
            {
                message = "Invalid Email or Password"
            });

        return Ok(new { token = token });
    }

}