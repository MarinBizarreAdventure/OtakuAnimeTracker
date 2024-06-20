using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.Animes.Commands;
using OtakuTracker.Application.Animes.Queries;
using OtakuTracker.Application.Animes.Responses;
using Swashbuckle.AspNetCore.Annotations;
using OtakuTracker.Application.Animes.Create;
using OtakuTracker.Application.Animes.Records;

namespace OtakuTracker.WebAPI.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnimeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new anime")]
    [SwaggerResponse(201, "Anime created successfully", typeof(AnimeDto))]
    [SwaggerResponse(400, "Invalid input")]
    public async Task<ActionResult<AnimeDto>> CreateAnime(CreateAnime command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAnimeById), new { id = result.AnimeId }, result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Gets an anime by ID")]
    [SwaggerResponse(200, "Anime retrieved successfully", typeof(AnimeDto))]
    [SwaggerResponse(404, "Anime not found")]
    public async Task<ActionResult<AnimeDto>> GetAnimeById(int id)
    {
        var request = new GetByIdAnime(id);
        var result = await _mediator.Send(request);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Updates an existing anime")]
    [SwaggerResponse(204, "Anime updated successfully")]
    [SwaggerResponse(404, "Anime not found")]
    public async Task<IActionResult> UpdateAnime(UpdateAnime command)
    {
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletes an anime by ID")]
    [SwaggerResponse(204, "Anime deleted successfully")]
    [SwaggerResponse(404, "Anime not found")]
    public async Task<IActionResult> DeleteAnime(int id)
    {
        var command = new DeleteAnime(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}