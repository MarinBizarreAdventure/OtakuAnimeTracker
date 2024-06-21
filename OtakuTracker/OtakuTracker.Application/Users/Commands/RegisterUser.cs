using System.Net.Http.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;
using OtakuTracker.Domain.Models.Auth;

namespace OtakuTracker.Application.Users.Commands;


public record RegisterUser(string Username, string Email, string Password) : IRequest<bool>;

public class RegisterUserHandler : IRequestHandler<RegisterUser, bool>
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

    public async Task<bool> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        
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

            await _unitOfWork.UserRepository.CreateUser(user);
            
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("User registered successfully");
            return true;  // Returning true on success
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Failed to register user");
            throw;
        }
    }
}