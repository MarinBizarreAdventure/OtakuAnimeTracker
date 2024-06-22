using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.Animes.Records;
using OtakuTracker.Application.Reviews.Commands;
using OtakuTracker.Application.Reviews.Queries;
using OtakuTracker.Application.Reviews.Responses;
using OtakuTracker.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new review")]
        [SwaggerResponse(201, "Review created successfully", typeof(Review))]
        [SwaggerResponse(400, "Invalid input")]
        public async Task<ActionResult<Review>> CreateReview(CreateReview command)
        {
            var createdReviewDto = await _mediator.Send(command);
            if (createdReviewDto.ReviewId != null)
            {
                return CreatedAtAction(nameof(GetReviewById), new { id = createdReviewDto.ReviewId }, createdReviewDto);
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a review by ID")]
        [SwaggerResponse(200, "Review retrieved successfully", typeof(Review))]
        [SwaggerResponse(404, "Review not found")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(int id)
        {
            var query = new GetReviewById(id);
            var review = await _mediator.Send(query);
            if (review == null)
            {
                return NotFound();
            }
            return review;
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Updates an existing review")]
        [SwaggerResponse(204, "Review updated successfully")]
        [SwaggerResponse(404, "Review not found")]
        public async Task<IActionResult> UpdateReview(UpdateReview command)
        {
            
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a review by ID")]
        [SwaggerResponse(204, "Review deleted successfully")]
        [SwaggerResponse(404, "Review not found")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var command = new DeleteReview(id);
            try
            {
                var isDeleted = await _mediator.Send(command);
                if(isDeleted)
                    return NoContent();
                else 
                    return NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return null;
        }
    }

}
