using Microsoft.AspNetCore.Mvc;

public class AuthLoginRequest
{
    [System.ComponentModel.DataAnnotations.Required]
    public string Username { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required]
    public string Password { get; set; } = string.Empty;
}

public class AuthLoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string Username { get; set; } = string.Empty;
}

[ApiController]
[Route("api/auth")]
public class LoginController : ControllerBase
{
    private readonly IAuthService _authService;

    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthLoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: Replace with real user lookup + password verification
        if (!IsValidUser(request.Username, request.Password))
            return Unauthorized(new { message = "Invalid username or password." });

        var token = _authService.GenerateToken(request.Username);

        return Ok(new AuthLoginResponse
        {
            Token = token,
            Username = request.Username,
            ExpiresIn = 3600 // seconds (matches the 1-hour expiry in AuthService)
        });
    }

    // Stub — replace with a real DB/identity check
    private static bool IsValidUser(string username, string password)
    {
        // Example only — never hard-code credentials in production
        return !string.IsNullOrWhiteSpace(username)
            && !string.IsNullOrWhiteSpace(password);
    }
}

