using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Reviews.Responses
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int AnimeId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public static ReviewDto FromReview(Review review)
        {
            return new ReviewDto
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                AnimeId = review.AnimeId,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                ReviewDate = review.ReviewDate
            };
        }
    }
}
