using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.Services;
using OtakuTracker.Application.Users.Commands;

namespace OtakuTracker.WebAPI.Controllers;

[Route("auth/")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IMediator _mediator;

    public AuthenticationController(ITokenService tokenService, IMediator mediator)
    {
        _tokenService = tokenService;
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("test")]
    public Task<IActionResult> Test()
    {
        return Task.FromResult<IActionResult>(Ok("Hello World"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUser command)
    {
        try
        {
            var userDto = await _mediator.Send(command);

            var claims = new[]
            {
                new Claim(ClaimTypes.UserData, userDto.UserId.ToString()),
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshExpiry = refreshToken.ExpiryDate
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUser command)
    {
        try
        {
            var userDto = await _mediator.Send(command);

            var claims = new[]
            {
                new Claim(ClaimTypes.UserData, userDto.UserId.ToString()),
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshExpiry = refreshToken.ExpiryDate
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}