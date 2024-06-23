using System.Text.Json.Nodes;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Domains.ElasticSearch.Queries;

public record SearchAnimeQuery(string Query, int From, int Size) : IRequest<JsonObject>;

public class SearchAnimeQueryHandler : IRequestHandler<SearchAnimeQuery, JsonObject>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SearchAnimeQueryHandler> _logger;

    public SearchAnimeQueryHandler(IUnitOfWork unitOfWork, ILogger<SearchAnimeQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<JsonObject> Handle(SearchAnimeQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling anime search query");

        try
        {
            var searchResult = await _unitOfWork.ElasticAnimeRepository.SearchAnime(request.Query, request.From, request.Size);

            if (searchResult == null)
            {
                _logger.LogWarning("No search results found");
                return null;
            }

            _logger.LogInformation("Anime search query completed successfully");
            return searchResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching anime");
            throw;
        }
    }
}