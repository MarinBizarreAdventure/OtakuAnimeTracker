// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Application.Users.Responses;
// using OtakuTracker.Domain.Models;
//
// namespace OtakuTracker.Application.Users.Commands
// {
//     public record CreateUser(string Username, string Email, string PasswordHash, DateTime JoinDate, DateTime LastLogin) : IRequest<UserDto>;
//
//     public class CreateUserHandler(
//         IUnitOfWork unitOfWork, 
//         ILogger<CreateUserHandler> logger)
//         : IRequestHandler<CreateUser, UserDto>
//     {
//         public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
//         {
//             logger.LogInformation("Handling request to create user");
//
//             var user = new User
//             {
//                 username = request.Username,
//                 email = request.Email,
//                 passwordhash = request.PasswordHash,
//                 joindate = request.JoinDate,
//                 lastlogin = request.LastLogin
//             };
//
//             await unitOfWork.BeginTransactionAsync();
//
//             try
//             {
//                 var createdUser = unitOfWork.UserRepository.CreateUser(user);
//                 await unitOfWork.CommitTransactionAsync();
//                 logger.LogInformation("User created successfully");
//                 return UserDto.FromUser(createdUser);
//             }
//             catch (Exception ex)
//             {
//                 await unitOfWork.RollbackTransactionAsync();
//                 logger.LogError(ex, "Failed to create user");
//                 throw;
//             }
//         }
//     }
// }
