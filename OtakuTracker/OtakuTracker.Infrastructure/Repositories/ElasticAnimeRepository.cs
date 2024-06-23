using Nest;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

public class ElasticAnimeRepository : IElasticAnimeRepository
{
    private readonly IElasticClient _elasticClient;
    private readonly ILogger<ElasticAnimeRepository> _logger;

    public ElasticAnimeRepository(IElasticClient elasticClient, ILogger<ElasticAnimeRepository> logger)
    {
        _elasticClient = elasticClient;
        _logger = logger;
    }

    public async Task<JsonObject> SearchAnime(string query, int from, int size)
    {
        try
        {
            // Define the raw JSON query
            var rawQuery = new
            {
                from,
                size,
                query = new
                {
                    simple_query_string = new
                    {
                        query,
                        fields = new[] { "*" },
                        default_operator = "AND"
                    }
                }
            };

            // Serialize the raw query to JSON string
            var rawQueryJson = JsonSerializer.Serialize(rawQuery);

            // Send the raw JSON query using the low-level client
            var response = await _elasticClient.LowLevel.SearchAsync<StringResponse>("anime", PostData.String(rawQueryJson));

            // Check if the response is successful
            if (!response.Success)
            {
                _logger.LogError("Elasticsearch search error: {Reason}", response.DebugInformation);
                return null;
            }

            // Parse the raw JSON response into a JsonObject
            var jsonResponse = JsonNode.Parse(response.Body) as JsonObject;

            return jsonResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching anime");
            throw;
        }
    }
}