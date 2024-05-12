using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsTokenValid()
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies["JWTToken"];
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Check if the token has expired
            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
        catch
        {
            // Log exception or handle token read error
            return false;
        }

    }

    public string GetUserIdFromToken()
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies["JWTToken"];
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }
        catch
        {
            // Handle or log exception
            return null;
        }
    }


}
