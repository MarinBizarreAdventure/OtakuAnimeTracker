using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Users.Commands
{
    public record DeleteUser(int UserId) : IRequest<bool>;

    public class DeleteUserHandler : IRequestHandler<DeleteUser, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteUserHandler> _logger;

        public DeleteUserHandler(IUnitOfWork unitOfWork, ILogger<DeleteUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling request to delete user with ID: {UserId}", request.UserId);

                await _unitOfWork.BeginTransactionAsync();

                var deletionResult = await _unitOfWork.UserRepository.DeleteUser(request.UserId);

                if (deletionResult)
                {
                    await _unitOfWork.CommitTransactionAsync();
                    _logger.LogInformation("User deleted successfully");
                }
                else
                {
                    _logger.LogInformation("User with ID {UserId} not found", request.UserId);
                }

                return deletionResult;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                var errorMessage = $"Failed to delete user with ID: {request.UserId}";
                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage);
            }
        }
        
    }


}
