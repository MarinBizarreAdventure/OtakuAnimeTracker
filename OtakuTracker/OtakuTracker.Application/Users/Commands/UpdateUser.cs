// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Domain.Models;
//
// namespace OtakuTracker.Application.Users.Commands
// {
//     public record UpdateUser(int UserId, string Username, string Email, string PasswordHash) : IRequest<User>;
//
//     public class UpdateUserHandler(IUnitOfWork unitOfWork, ILogger<UpdateUserHandler> logger)
//         : IRequestHandler<UpdateUser, User>
//     {
//         public async Task<User?> Handle(UpdateUser request, CancellationToken cancellationToken)
//         {
//             logger.LogInformation("Handling request to update user");
//
//             await unitOfWork.BeginTransactionAsync();
//
//             try
//             {
//                 var user = new User
//                 {
//                     userid = request.UserId,
//                     username = request.Username,
//                     email = request.Email,
//                     passwordhash = request.PasswordHash,
//                 };
//
//                 var updatedUser = unitOfWork.UserRepository.UpdateUser(user);
//                 await unitOfWork.CommitTransactionAsync();
//                 logger.LogInformation("User updated successfully");
//                 return updatedUser;
//             }
//             catch (Exception ex)
//             {
//                 await unitOfWork.RollbackTransactionAsync();
//                 logger.LogError(ex, "Failed to update user");
//                 throw;
//             }
//         }
//     }
// }
