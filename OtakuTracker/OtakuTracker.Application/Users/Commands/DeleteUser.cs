// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
//
// namespace OtakuTracker.Application.Users.Commands
// {
//     public record DeleteUser(int UserId) : IRequest<bool>;
//
//     public class DeleteUserHandler(
//         IUnitOfWork unitOfWork, 
//         ILogger<DeleteUserHandler> logger)
//         : IRequestHandler<DeleteUser, bool>
//     {
//         public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
//         {
//             logger.LogInformation("Handling request to delete user with ID: {UserId}", request.UserId);
//
//             await unitOfWork.BeginTransactionAsync();
//
//             try
//             {
//                 unitOfWork.UserRepository.DeleteUser(request.UserId);
//                 await unitOfWork.CommitTransactionAsync();
//                 logger.LogInformation("User deleted successfully");
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 await unitOfWork.RollbackTransactionAsync();
//                 logger.LogError(ex, "Failed to delete user with ID: {UserId}", request.UserId);
//                 return false;
//             }
//         }
//     }
// }
