using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Users.Responses;
using OtakuTracker.Domain.Models.Auth;

namespace OtakuTracker.Application.Users.Commands;

public record LoginUser(
    [Required(ErrorMessage = "Username is required.")]
    string Username,
    
    [Required(ErrorMessage = "Password is required.")]
    string Password
) : IRequest<UserDto>;
public class LoginUserHandler : IRequestHandler<LoginUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginUserHandler> _logger;
    private readonly IMapper _mapper;

    public LoginUserHandler(IUnitOfWork unitOfWork, ILogger<LoginUserHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _logger.LogInformation("Handling login request for username: {Username}", request.Username);

            var user = await _unitOfWork.UserRepository.GetUserByUsername(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Passwordhash))
            {
                _logger.LogWarning("Invalid username or password for username: {Username}", request.Username);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("User with username: {Username} logged in successfully", request.Username);

            return _mapper.Map<UserDto>(user);  
        }
        catch (UnauthorizedAccessException ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogWarning(ex, "Unauthorized access attempt for username: {Username}", request.Username);
            throw;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Failed to log in user with username: {Username}", request.Username);
            throw new Exception("An error occurred while processing your request. Please try again later.");
        }
    }
}