using BursaryFinderAPI.Dtos;
using BursaryFinderAPI.Models;
using BursaryFinderAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BursaryFinderAPI.Controllers;

[ApiController]
[Route("/api/")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("users")]
    public List<User> GetUsers() => _userService.GetUsers();

    [HttpGet("users/{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);
        
        if (user is null)
            return NotFound();
        
        return Ok(user);
    }

    [HttpPut("users/{id}")]
    public async Task<ActionResult> UpdateUser(int id, UpdateUserDto dto)
    {
        var user = await _userService.UpdateUserById(id, dto);

        if (user is null)
            return NotFound();
        
        return StatusCode(204);
    }

    [HttpDelete("users/{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUserById(id);

        if (user is null)
            return NotFound();

        return Ok();
    }

}