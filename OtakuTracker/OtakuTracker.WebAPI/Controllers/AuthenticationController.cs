using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.Services;
using OtakuTracker.Application.Users.Commands;

namespace OtakuTracker.WebAPI.Controllers;

[Route("auth/")]
[ApiController]
public class AuthenticationController(ITokenService tokenService, IMediator mediator) : ControllerBase
{
    
    [Authorize]
    [HttpGet("test")]
    public Task<IActionResult> Test()
    {
        return Task.FromResult<IActionResult>(Ok("Hello World"));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUser command)
    {
        
        bool state = await mediator.Send(command);
        
        if (state == false)
        {
            return NotFound();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, command.Username)
        };

        var accessToken = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();

        return await Task.FromResult<IActionResult>(
            Ok(
                new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    RefreshExpiry = refreshToken.ExpiryDate
                }));
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Login(RegisterUser command)
    {
        
        bool state = await mediator.Send(command);

        if (state == false)
        {
            return NotFound();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, command.Username)
        };

        var accessToken = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();

        return await Task.FromResult<IActionResult>(
            Ok(
                new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    RefreshExpiry = refreshToken.ExpiryDate
                }));
    }
}