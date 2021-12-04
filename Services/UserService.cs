
using BursaryFinderAPI.Context;
using BursaryFinderAPI.Dtos;
using BursaryFinderAPI.Models;

namespace BursaryFinderAPI.Services;
public class UserService : IUserService
{
    private readonly BursaryFinderContext _ctx;
    public UserService(BursaryFinderContext context)
    {
        _ctx = context;
    }
    public async Task<User> DeleteUserById(int id)
    {
        var user = _ctx.Users.FirstOrDefault<User>(u => u.Id == id);

        if (user is null)
            return null;
        
        _ctx.Users.Remove(user);
        await _ctx.SaveChangesAsync();

        return user;

    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _ctx.Users.FindAsync(id);

        if (user is null)
            return null;
        
        return user;
    }

    public List<User> GetUsers() => _ctx.Users.ToList();

    public async Task<User> UpdateUserById(int id, UpdateUserDto dto)
    {
        var user = await _ctx.Users.FindAsync(id);

        if (user is null)
            return null;
        
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Bio = dto.Bio;
        user.PhoneNumber = dto.PhoneNumber;

        _ctx.Users.Update(user);
        await _ctx.SaveChangesAsync();

        return user;
    }
}

public interface IUserService
{
    List<User> GetUsers();

    Task<User> GetUserById(int id);

    Task<User> UpdateUserById(int id, UpdateUserDto dto);

    Task<User> DeleteUserById(int id);

}