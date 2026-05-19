using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    /// <summary>
    /// Validates the provided credentials and returns a signed access token when successful.
    /// </summary>
    /// <param name="request">The sign-in request payload.</param>
    /// <returns>An access token when credentials are valid; otherwise an error response.</returns>
    [AllowAnonymous]
    [HttpPost("signin")]
    public IActionResult Signin([FromBody] AuthModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Email and password are required.");
        }

        var token = authService.Signin(request.Email, request.Password);
        if (token is null)
        {
            return Unauthorized("Invalid credentials.");
        }

        Response.Cookies.Append("session", token.AccessToken, new Microsoft.AspNetCore.Http.CookieOptions
        {
            HttpOnly = true,
            Secure = HttpContext.Request.IsHttps,
            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax,
            Expires = new DateTimeOffset(token.ExpiresAtUtc)
        });

        return Ok(token);
    }

    /// <summary>
    /// Verifies that the current request is authenticated by a valid access token.
    /// </summary>
    /// <returns>The authenticated state and available identity claims for the current user.</returns>
    [Authorize]
    [HttpGet("is-authenticated")]
    [ProducesResponseType<AuthStatusResult>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<AuthStatusResult> IsAuthenticated()
    {
        var email = User.FindFirstValue(JwtRegisteredClaimNames.Email)
            ?? User.FindFirstValue(ClaimTypes.Email)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new AuthStatusResult(true, email, role));
    }
}
