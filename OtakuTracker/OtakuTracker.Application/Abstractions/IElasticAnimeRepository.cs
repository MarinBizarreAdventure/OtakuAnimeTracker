using System.Text.Json.Nodes;

namespace OtakuTracker.Application.Abstractions;

public interface IElasticAnimeRepository
{
    Task<JsonObject> SearchAnime(string query, int from, int size);

}