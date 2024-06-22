using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.Domains.AnimeGenres.Queries;
using OtakuTracker.Application.Domains.AnimeGenres.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuTracker.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeGenreController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnimeGenreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Creates anime genres")]
    [SwaggerResponse(200, "Anime genres created successfully", typeof(List<AnimeGenreDto>))]
    [SwaggerResponse(400, "Invalid input")]
    public async Task<ActionResult<List<AnimeGenreDto>>> CreateAnimeGenres(CreateAnimeGenres command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("anime/{animeId}")]
    [SwaggerOperation(Summary = "Gets all genre IDs associated with an anime")]
    [SwaggerResponse(200, "Genre IDs retrieved successfully", typeof(List<int>))]
    [SwaggerResponse(404, "Anime not found")]
    public async Task<ActionResult<List<int>>> GetAllGenreIdsByAnimeId(int animeId)
    {
        var request = new GetAllGenreIdsByAnimeId(animeId);
        var result = await _mediator.Send(request);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    
}