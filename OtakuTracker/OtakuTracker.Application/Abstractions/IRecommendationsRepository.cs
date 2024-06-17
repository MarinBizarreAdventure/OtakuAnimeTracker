using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IRecommendationsRepository
    {
        Recommendation CreateRecommendation(Recommendation recommendation);
        Recommendation GetRecommendationById(int recommendationId);
        List<Recommendation> GetAllRecommendations(); 
        List<Recommendation > GetRecommendationsByUserId(int userId); 
        List<Recommendation> GetRecommendationsByAnimeId(int animeId); 
        void UpdateRecommendation(Recommendation recommendation); 
        void DeleteRecommendation(int recommendationId); 
        
    }
}
