using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Users.Responses;

namespace OtakuTracker.Application.Users.Queries
{
    public record GetUserByUsername(string Username) : IRequest<UserDto>;
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsername, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetUserByUsernameHandler> _logger;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(IUnitOfWork unitOfWork, ILogger<GetUserByUsernameHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByUsername request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling request to get user by username: {Username}", request.Username);

                var user = await _unitOfWork.UserRepository.GetUserByUsername(request.Username);

                if (user == null)
                {
                    var notFoundMessage = $"User with username {request.Username} not found";
                    _logger.LogWarning(notFoundMessage);
                    throw new KeyNotFoundException(notFoundMessage); // You can use a specific exception type if needed
                }

                _logger.LogInformation("User with username {Username} found", request.Username);
                return UserDto.FromUser(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                throw new Exception(ex.Message); // Re-throw specific exception
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while retrieving user by username";
                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}