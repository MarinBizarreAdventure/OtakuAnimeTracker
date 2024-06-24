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

            var rawQueryJson = JsonSerializer.Serialize(rawQuery);

            var response =
                await _elasticClient.LowLevel.SearchAsync<StringResponse>("anime", PostData.String(rawQueryJson));
            if (!response.Success)
            {
                _logger.LogError("Elasticsearch search error: {Reason}", response.DebugInformation);
                return null;
            }

            var jsonResponse = JsonNode.Parse(response.Body) as JsonObject;

            return jsonResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching anime");
            throw;
        }
    }


    // public static async Task SendRequestToElasticsearch(string jsonRequest)
    // {
    //     // Your Elasticsearch URL
    //     string elasticSearchUrl = "http://your-elasticsearch-url:9200/your-index/_search";
    //
    //     using (var client = new HttpClient())
    //     {
    //         // Prepare HTTP request content
    //         var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
    //
    //         // Send the POST request to Elasticsearch
    //         var response = await client.PostAsync(elasticSearchUrl, content);
    //
    //         // Check if the request was successful
    //         if (response.IsSuccessStatusCode)
    //         {
    //             // Handle successful response
    //             var responseContent = await response.Content.ReadAsStringAsync();
    //             Console.WriteLine("Elasticsearch response:");
    //             Console.WriteLine(responseContent);
    //         }
    //         else
    //         {
    //             // Handle error response
    //             Console.WriteLine($"Error: {response.StatusCode}");
    //         }
    //     }
    // }
}