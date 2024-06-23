using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Users.Responses;
using OtakuTracker.Domain.Models;
using OtakuTracker.Domain.Models.Auth;

namespace OtakuTracker.Application.Users.Commands;


public record RegisterUser(
    [Required(ErrorMessage = "Username is required.")]
    string Username,
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    string Email,
    
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    string Password
) : IRequest<UserDto>;

public class RegisterUserHandler : IRequestHandler<RegisterUser, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly IMapper _mapper;

    public RegisterUserHandler(IUnitOfWork unitOfWork, ILogger<RegisterUserHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        _logger.LogInformation("Register operation started");

        try
        {
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                Passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                JoinDate = DateTime.UtcNow,
                LastOnline = DateTime.UtcNow
            };

            user = await _unitOfWork.UserRepository.CreateUser(user);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("User registered successfully");

            return UserDto.FromUser(user);  // Returning UserDto on success
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            var errorMessage = "Failed to register user";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}