using AutoMapper;
using MediatR;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Domain.Models;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Animes.Records;


namespace OtakuTracker.Application.Animes.Commands;


public class UpdateAnimeHandler : IRequestHandler<UpdateAnime, AnimeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateAnimeHandler> _logger;
        private readonly IMapper _mapper;
        
        
        public UpdateAnimeHandler(IUnitOfWork unitOfWork, ILogger<UpdateAnimeHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AnimeDto> Handle(UpdateAnime request, CancellationToken cancellationToken)
        {

            var updatedAnime =_mapper.Map<Anime>(request);;

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _unitOfWork.AnimeRepository.Update(updatedAnime);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Anime updated successfully");
                return AnimeDto.FromAnime(updatedAnime);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Failed to update anime");
                throw;
            }
        }
    }

    
