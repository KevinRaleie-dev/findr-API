using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;

namespace BursaryFinderAPI.Helpers;
public class JwtAuth : IJwtAuth
{
    private const string secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
    public String CreateToken(int id, string role)
    {
        var payload = new Dictionary<string, object>
        {
            {"id", id},
            {"role", role}
        };

        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        var token = encoder.Encode(payload, secretKey);

        return token;
    }

    public IDictionary<string, object> VerifyToken(string token)
    {
        var payload = JwtBuilder.Create()
                                .WithAlgorithm(new HMACSHA256Algorithm())
                                .WithSecret(secretKey)
                                .MustVerifySignature()
                                .Decode<IDictionary<string, object>>(token);

        return payload;
    }
}

public interface IJwtAuth
{
    String CreateToken(int id, string role);

    IDictionary<string, object> VerifyToken(string token);
}