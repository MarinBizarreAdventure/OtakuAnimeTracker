using System.Net.Http.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models.Auth;

namespace OtakuTracker.Application.Users.Commands;

public record LoginUser(string Username, string Password) : IRequest<bool>;

public class LoginUserHandler(IUnitOfWork unitOfWork, ILogger<LoginUserHandler> logger) : IRequestHandler<LoginUser, bool>
{
    public async Task<bool> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var state = false;
            var user = unitOfWork.UserRepository.GetUserByUsername(request.Username);
            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            
            if (BCrypt.Net.BCrypt.Verify(hashedPassword, user.passwordhash))
            {
                state = true;
            }
            
            await unitOfWork.CommitTransactionAsync();
            logger.LogInformation("User checked successfully");
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