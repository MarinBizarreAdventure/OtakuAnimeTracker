using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtakuTracker.Application.Genres.Queries;
using OtakuTracker.Application.Genres.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuTracker.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a genre by ID")]
    [SwaggerResponse(200, "Genre retrieved successfully", typeof(GenreDto))]
    [SwaggerResponse(404, "Genre not found")]
    public async Task<ActionResult<GenreDto>> GetGenreById(int id)
    {
        var request = new GetGenreById(id);
        var result = await _mediator.Send(request);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all genres")]
    [SwaggerResponse(200, "Genres retrieved successfully", typeof(List<GenreDto>))]
    public async Task<ActionResult<List<GenreDto>>> GetAllGenres()
    {
        var request = new GetAllGenres();
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}     