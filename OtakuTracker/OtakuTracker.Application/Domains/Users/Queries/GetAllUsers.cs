// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Application.Users.Responses;
// using OtakuTracker.Domain.Models;
//
// namespace OtakuTracker.Application.Users.Queries
// {
//     public record GetAllUsers : IRequest<List<UserDto>>;
//
//     public class GetAllUsersHandler(IUnitOfWork unitOfWork, ILogger<GetAllUsersHandler> logger)
//         : IRequestHandler<GetAllUsers, List<UserDto>>
//     {
//         public async Task<List<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
//         {
//             logger.LogInformation("Handling request to get all users");
//
//             var users = unitOfWork.UserRepository.GetAllUsers();
//             var userDtos = users.Select(user => UserDto.FromUser(user)).ToList();
//
//             logger.LogInformation("Retrieved {UserCount} users", userDtos.Count);
//
//             return userDtos;
//         }
//     }
//
// }
