using BursaryFinderAPI.Context;
using BursaryFinderAPI.Dtos;
using BursaryFinderAPI.Helpers;
using BursaryFinderAPI.Models;

namespace BursaryFinderAPI.Services;
public class AuthService : IAuthService
{

    private readonly BursaryFinderContext _ctx;
    private IJwtAuth _jwt;

    private IHasher _hash;

    public AuthService(BursaryFinderContext context, IJwtAuth jwtAuth, IHasher hash)
    {
        _ctx = context;
        _jwt = jwtAuth;
        _hash = hash;
    }
    public string LoginOrganization(LoginDto dto)
    {
        var organization = _ctx.Organization.FirstOrDefault<Organization>(org => org.Email == dto.Email);

        if (organization is null)
            return null;

        var isValidPassword = _hash.Verify(dto.Password, organization.Password);

        if (!isValidPassword)
            return null;

        var token = _jwt.CreateToken(organization.Id, "organization");

        return token;
    }

    public string LoginUser(LoginDto dto)
    {
        var user = _ctx.Users.FirstOrDefault<User>(u => u.Email == dto.Email);

        if (user is null)
            return null;

        var isValidPassword = _hash.Verify(dto.Password, user.Password);

        if (!isValidPassword)
            return null;

        var token = _jwt.CreateToken(user.Id, "student");

        return token;
    }

    public async Task<Organization> RegisterOrganization(Organization organization)
    {
        var entity = _ctx.Organization.FirstOrDefault<Organization>(org => org.Email == organization.Email);

        if (entity is not null)
        {
            return null;
        }
        else
        {   
            organization.Password = _hash.Hash(organization.Password);

            await _ctx.Organization.AddAsync(organization);

            await _ctx.SaveChangesAsync();

            return organization;
        }

    }

    public async Task<User> RegisterUser(User user)
    {
        var entity = _ctx.Users.FirstOrDefault(u => u.Email == user.Email);

        if (entity is not null)
        {
            return null;
        }
        else
        {
            user.Password = _hash.Hash(user.Password);

            await _ctx.Users.AddAsync(user);

            await _ctx.SaveChangesAsync();

            return user;
        }
    }
}

public interface IAuthService
{
    Task<User> RegisterUser(User user);
    Task<Organization> RegisterOrganization(Organization organization);

    String LoginUser(LoginDto dto);

    String LoginOrganization(LoginDto dto);
}