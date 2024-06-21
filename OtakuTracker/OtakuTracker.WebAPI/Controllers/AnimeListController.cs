using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.AnimeLists.Commands;
using OtakuTracker.Application.AnimeLists.Queries;
using OtakuTracker.Application.AnimeLists.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuTracker.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeListController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnimeListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{username}")]
    [SwaggerOperation(Summary = "Gets an anime list by username")]
    [SwaggerResponse(200, "Anime list retrieved successfully", typeof(List<AnimeListDto>))]
    [SwaggerResponse(404, "Anime list not found")]
    public async Task<ActionResult<List<AnimeListDto>>> GetAnimeListByUsername(string username)
    {
        var request = new GetAnimeList(username);
        var result = await _mediator.Send(request);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new anime list")]
    [SwaggerResponse(201, "Anime list created successfully", typeof(AnimeListDto))]
    [SwaggerResponse(400, "Invalid input")]
    public async Task<ActionResult<AnimeListDto>> CreateAnimeList(CreateAnimeList command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAnimeListByUsername), new { username = result.Username }, result);
    }

    [HttpDelete("{username}/{animeId}")]
    [SwaggerOperation(Summary = "Removes an anime from user's list")]
    [SwaggerResponse(204, "Anime removed successfully")]
    [SwaggerResponse(404, "Anime or user not found")]
    public async Task<IActionResult> RemoveAnimeFromUserList(string username, int animeId)
    {
        var command = new RemoveAnimeFromUserList(username, animeId);
        await _mediator.Send(command);
        return NoContent();
    }
}