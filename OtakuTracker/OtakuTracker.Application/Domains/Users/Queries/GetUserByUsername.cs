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
            _logger.LogInformation("Handling request to get user by username: {Username}", request.Username);

            var user = await _unitOfWork.UserRepository.GetUserByUsername(request.Username);

            if (user == null)
            {
                _logger.LogInformation("User with username {Username} not found", request.Username);
                return null;
            }

            _logger.LogInformation("User with username {Username} found", request.Username);
            return UserDto.FromUser(user);
        }
    }
}