using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions;

public interface IRatingCompleteRepository
{
    Task<RatingComplete> CreateRatingComplete(RatingComplete ratingComplete);
    Task<RatingComplete> GetRatingCompleteById(int ratingCompleteId);
    Task<List<RatingComplete>> GetAllRatingCompletes();
    Task UpdateRatingComplete(RatingComplete ratingComplete);
    Task DeleteRatingComplete(int ratingCompleteId);
}