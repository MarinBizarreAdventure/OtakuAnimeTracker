﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Users.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Users.Queries;

public record GetUserById(int UserId) : IRequest<UserDto>;

public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetUserByIdHandler> _logger;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUnitOfWork unitOfWork, ILogger<GetUserByIdHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request to get user by ID: {UserId}", request.UserId);

        var user = await _unitOfWork.UserRepository.GetUserById(request.UserId);

        if (user == null)
        {
            _logger.LogInformation("User with ID {UserId} not found", request.UserId);
            return null;
        }

        _logger.LogInformation("User with ID {UserId} found", request.UserId);
        return UserDto.FromUser(user);
    }
}