using System.Net.Http.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Users.Responses;
using OtakuTracker.Domain.Models.Auth;

namespace OtakuTracker.Application.Users.Commands;

public record LoginUser(string Username, string Password) : IRequest<UserDto>;

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
            var user = await _unitOfWork.UserRepository.GetUserByUsername(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Passwordhash))
            {
                _logger.LogWarning("Invalid username or password");
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("User logged in successfully");

            return _mapper.Map<UserDto>(user);  
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Failed to log in user");
            throw;
        }
    }
}