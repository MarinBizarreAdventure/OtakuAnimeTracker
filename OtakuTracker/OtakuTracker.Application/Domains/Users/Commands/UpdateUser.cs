using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Users.Commands
{
    public record UpdateUser(
    int UserId,
    
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    string? Email,
    
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    string? PasswordHash,
    
    [Range(0, int.MaxValue, ErrorMessage = "UserWatching must be a non-negative integer.")]
    int? UserWatching,
    
    [Range(0, int.MaxValue, ErrorMessage = "UserCompleted must be a non-negative integer.")]
    int? UserCompleted,
    
    [Range(0, int.MaxValue, ErrorMessage = "UserOnhold must be a non-negative integer.")]
    int? UserOnhold,
    
    [Range(0, int.MaxValue, ErrorMessage = "UserDropped must be a non-negative integer.")]
    int? UserDropped,
    
    [Range(0, int.MaxValue, ErrorMessage = "UserPlantowatch must be a non-negative integer.")]
    int? UserPlantowatch,
    
    [Range(0, double.MaxValue, ErrorMessage = "UserDaysSpentWatching must be a non-negative number.")]
    double? UserDaysSpentWatching,
    
    [StringLength(10, ErrorMessage = "Gender must be at most 10 characters long.")]
    string? Gender,
    
    [StringLength(50, ErrorMessage = "Location must be at most 50 characters long.")]
    string? Location,
    
    [DataType(DataType.Date, ErrorMessage = "BirthDate must be a valid date.")]
    DateOnly? BirthDate,
    
    [Range(0, int.MaxValue, ErrorMessage = "AccessRank must be a non-negative integer.")]
    int? AccessRank,
    
    [DataType(DataType.DateTime, ErrorMessage = "JoinDate must be a valid date and time.")]
    DateTime? JoinDate,
    
    [DataType(DataType.DateTime, ErrorMessage = "LastOnline must be a valid date and time.")]
    DateTime? LastOnline,
    
    [Range(0, double.MaxValue, ErrorMessage = "StatsMeanScore must be a non-negative number.")]
    double? StatsMeanScore,
    
    [Range(0, int.MaxValue, ErrorMessage = "StatsRewatched must be a non-negative integer.")]
    int? StatsRewatched,
    
    [Range(0, int.MaxValue, ErrorMessage = "StatsEpisodes must be a non-negative integer.")]
    int? StatsEpisodes
) : IRequest<User>;

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
