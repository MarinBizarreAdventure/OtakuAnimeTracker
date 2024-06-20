// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Application.Users.Responses;
// using OtakuTracker.Domain.Models;
//
// namespace OtakuTracker.Application.Users.Queries;
//
// public record GetUserById(int UserId) : IRequest<UserDto>;
//
// public class GetUserByIdHandler(IUnitOfWork unitOfWork, ILogger<GetUserByIdHandler> logger)
//     : IRequestHandler<GetUserById, UserDto>
// {
//     public Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
//     {
//         logger.LogInformation("Handling request to get user by ID: {UserId}", request.UserId);
//
//         var user = unitOfWork.UserRepository.GetUserById(request.UserId);
//
//         logger.LogInformation("User with ID {UserId} found", request.UserId);
//         return Task.FromResult(UserDto.FromUser(user));
//     }
// }