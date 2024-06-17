using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Review>> CreateReview(Review review)
        {
            //var command = new CreateReviewCommand(review);
            //var createdReview = await _mediator.Send(command);
            //return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.ReviewId }, createdReview);
            return null;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a review by ID")]
        [SwaggerResponse(200, "Review retrieved successfully", typeof(Review))]
        [SwaggerResponse(404, "Review not found")]
        public async Task<ActionResult<Review>> GetReviewById(int id)
        {
            //var query = new GetReviewByIdQuery(id);
            //var review = await _mediator.Send(query);
            //if (review == null)
            //{
            //    return NotFound();
            //}
            //return review;
            return null;
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing review")]
        [SwaggerResponse(204, "Review updated successfully")]
        [SwaggerResponse(404, "Review not found")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            //var command = new UpdateReviewCommand(id, review);
            //try
            //{
            //    await _mediator.Send(command);
            //    return NoContent();
            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
            //TO DO
            return null; 
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a review by ID")]
        [SwaggerResponse(204, "Review deleted successfully")]
        [SwaggerResponse(404, "Review not found")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            //var command = new DeleteReviewCommand(id);
            //try
            //{
            //    await _mediator.Send(command);
            //    return NoContent();
            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
            return null;
        }
    }

}
