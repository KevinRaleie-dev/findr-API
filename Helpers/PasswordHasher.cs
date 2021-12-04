using BC = BCrypt.Net;

namespace BursaryFinderAPI.Helpers;

public class PasswordHasher : IHasher
{
    public string Hash(string password)
    {
        var salt = BC.BCrypt
                    .GenerateSalt(10);

        var hash = BC.BCrypt
                    .HashPassword(password, salt);

        return hash;
    }

    public bool Verify(string password, string hash)
    {
        return BC.BCrypt
                .Verify(password, hash);
                
    }
}

public interface IHasher
{
    String Hash(string password);

    Boolean Verify(string password, string hash);
}