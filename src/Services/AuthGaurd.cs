public interface IAuthGaud
{
    bool IsAuthenticated(string token);
}

public class AuthGuard : IAuthGaud
{
    private readonly IAuthService _authService;

    public AuthGuard(IAuthService authService)
    {
        _authService = authService;
    }

    public bool IsAuthenticated(string token)
    {
        return _authService.ValidateToken(token);
    }
}