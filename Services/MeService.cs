using BursaryFinderAPI.Context;
using BursaryFinderAPI.Dtos;
using BursaryFinderAPI.Helpers;
using BursaryFinderAPI.Models;

namespace BursaryFinderAPI.Services;

public class MeService : IMe
{
    private readonly BursaryFinderContext _context;
    private IJwtAuth _jwt;
    public MeService(BursaryFinderContext context, IJwtAuth jwtAuth)
    {
        _context = context;
        _jwt = jwtAuth;
    }
    public Organization GetCurrentOrganization(MeDto dto)
    {
        var id = GetID(dto);
        var organization = _context.Organization.FirstOrDefault<Organization>(entity => entity.Id == id);
        if (organization is null)
            return null;
        
        return organization;
    }

    public User GetCurrentUser(MeDto dto)
    {
        
        var id = GetID(dto);
        var user = _context.Users.FirstOrDefault<User>(entity => entity.Id == id);

        if (user is null)
            return null;
        
        return user;    

    }

    private int GetID(MeDto dto)
    {
        try
        {
            var payload = _jwt.VerifyToken(dto.Token);

            return int.Parse(payload["id"].ToString());
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}

public interface IMe
{
    User GetCurrentUser(MeDto dto);
    Organization GetCurrentOrganization(MeDto dto);
}