public interface ITokenService
{
    bool IsTokenValid();
    public string GetUserIdFromToken();
}
