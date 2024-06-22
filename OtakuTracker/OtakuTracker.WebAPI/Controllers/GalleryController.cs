using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.Animes.Queries;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Application.Domains.AnimeGenres.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuTracker.WebAPI.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IMediator mediator, ILogger<GalleryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("popular")]
        [SwaggerOperation(Summary = "Gets a paginated list of popular anime IDs")]
        [SwaggerResponse(200, "Popular anime IDs retrieved successfully", typeof(List<int>))]
        public async Task<ActionResult<List<int>>> GetPopularAnimes([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string sortOrder)
        {
            _logger.LogInformation("Request received for popular anime IDs with page {Page}, pageSize {PageSize}, and sortOrder {SortOrder}", page, pageSize, sortOrder);
            var request = new GetPopularAnimeIds(page, pageSize, sortOrder);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("ranked")]
        [SwaggerOperation(Summary = "Gets a paginated list of ranked anime IDs")]
        [SwaggerResponse(200, "Ranked anime IDs retrieved successfully", typeof(List<int>))]
        public async Task<ActionResult<List<int>>> GetRankedAnimes([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string sortOrder)
        {
            _logger.LogInformation("Request received for ranked anime IDs with page {Page}, pageSize {PageSize}, and sortOrder {SortOrder}", page, pageSize, sortOrder);
            var request = new GetRankedAnimeIds(page, pageSize, sortOrder);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("anime/{id}")]
        [SwaggerOperation(Summary = "Gets an anime summary by ID")]
        [SwaggerResponse(200, "Anime summary retrieved successfully", typeof(AnimeSummaryDto))]
        [SwaggerResponse(404, "Anime not found")]
        public async Task<ActionResult<AnimeSummaryDto>> GetAnimeSummaryById(int id)
        {
            _logger.LogInformation("Request received for anime summary with ID {AnimeId}", id);
            var request = new GetAnimeSummaryById(id);
            var result = await _mediator.Send(request);

            if (result == null)
            {
                _logger.LogWarning("Anime with ID {AnimeId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Anime summary with ID {AnimeId} retrieved successfully", id);
            return Ok(result);
        }
        
        [HttpGet("genre/{genreId}")]
        [SwaggerOperation(Summary = "Gets all anime IDs associated with a genre")]
        [SwaggerResponse(200, "Anime IDs retrieved successfully", typeof(List<int>))]
        [SwaggerResponse(404, "Genre not found")]
        public async Task<ActionResult<List<int>>> GetAllAnimeIdsByGenreId(int genreId, int page = 0, int pageSize = 10)
        {
            var request = new GetAllAnimeIdsByGenreId(genreId, page, pageSize);
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpGet("{page}")]
        [SwaggerOperation(Summary = "Gets a paginated list of ranked anime IDs")]
        [SwaggerResponse(200, "Ranked anime IDs retrieved successfully", typeof(List<int>))]
        public async Task<ActionResult<List<int>>> GetAnimeId(int page, [FromQuery] int pageSize)
        {
            var request = new GetRankedAnimeIds(page, pageSize, "");
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }