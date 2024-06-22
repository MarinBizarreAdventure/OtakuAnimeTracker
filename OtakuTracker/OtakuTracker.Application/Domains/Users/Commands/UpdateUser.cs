using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Users.Commands
{
    public record UpdateUser(
        int UserId,
        string? Email,
        string? PasswordHash,
        int? UserWatching,
        int? UserCompleted,
        int? UserOnhold,
        int? UserDropped,
        int? UserPlantowatch,
        double? UserDaysSpentWatching,
        string? Gender,
        string? Location,
        DateOnly? BirthDate,
        int? AccessRank,
        DateTime? JoinDate,
        DateTime? LastOnline,
        double? StatsMeanScore,
        int? StatsRewatched,
        int? StatsEpisodes) : IRequest<User>;

    public class UpdateUserHandler : IRequestHandler<UpdateUser, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request to update user with ID: {UserId}", request.UserId);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var user = await _unitOfWork.UserRepository.GetUserById(request.UserId);

                if (user == null)
                {
                    _logger.LogInformation("User with ID {UserId} not found", request.UserId);
                    return null;
                }

                // Map properties from UpdateUser to User entity
                _mapper.Map(request, user);

                // Update user in repository
                await _unitOfWork.UserRepository.UpdateUser(user);
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("User updated successfully");
                return user;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                var errorMessage = $"Failed to update user with ID: {request.UserId}";
                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }

}
