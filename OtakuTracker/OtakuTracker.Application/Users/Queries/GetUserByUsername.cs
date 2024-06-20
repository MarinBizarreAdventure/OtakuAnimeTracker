// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Application.Users.Responses;
//
// namespace OtakuTracker.Application.Users.Queries
// {
//     public record GetUserByUsername(string Username) : IRequest<UserDto>;
//     public class GetUserByUsernameHandler(IUnitOfWork unitOfWork, ILogger<GetUserByUsernameHandler> logger)
//         : IRequestHandler<GetUserByUsername, UserDto>
//     {
//         public Task<UserDto> Handle(GetUserByUsername request, CancellationToken cancellationToken)
//         {
//             logger.LogInformation("Handling request to get user by username: {Username}", request.Username);
//
//             var user = unitOfWork.UserRepository.GetUserByUsername(request.Username);
//
//             if (user == null)
//             {
//                 logger.LogInformation("User with username {Username} not found", request.Username);
//                 return Task.FromResult<UserDto>(null);
//             }
//
//             logger.LogInformation("User with username {Username} found", request.Username);
//             return Task.FromResult(UserDto.FromUser(user));
//         }
//     }
// }