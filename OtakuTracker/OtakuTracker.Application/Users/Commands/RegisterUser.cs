using System.Net.Http.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;
using OtakuTracker.Domain.Models.Auth;

namespace OtakuTracker.Application.Users.Commands;

public record RegisterUser(string Username, string Email, string Password) : IRequest<bool>;

public class RegisterUserHandler(IUnitOfWork unitOfWork, ILogger<LoginUserHandler> logger) : IRequestHandler<RegisterUser, bool>
{
    public async Task<bool> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var state = false;

            var user = new User
            {
                email = request.Email,
                username = request.Username,
                passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                joindate = new DateTime().Date,
                lastlogin = new DateTime().Date
            };

            unitOfWork.UserRepository.CreateUser(user);
            
            await unitOfWork.CommitTransactionAsync();
            logger.LogInformation("User registered successfully");
            return state;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            logger.LogError(ex, "Failed to check user");
            throw;
        }
        
    }
}