// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using OtakuTracker.Application.Users.Commands;
// using OtakuTracker.Application.Users.Queries;
// using OtakuTracker.Application.Users.Responses;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace OtakuTracker.WebAPI.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class UserController(IMediator mediator) : ControllerBase
//     {
//         [HttpPost]
//         [SwaggerOperation(Summary = "Creates a new user")]
//         [SwaggerResponse(201, "User created successfully", typeof(UserDto))]
//         [SwaggerResponse(400, "Invalid input")]
//         public async Task<ActionResult<UserDto>> CreateUser(CreateUser command)
//         {
//             var createdUser = await mediator.Send(command);
//             return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
//         }
//
//         [HttpGet("{id}")]
//         [SwaggerOperation(Summary = "Gets a user by ID")]
//         [SwaggerResponse(200, "User retrieved successfully", typeof(UserDto))]
//         [SwaggerResponse(404, "User not found")]
//         public async Task<ActionResult<UserDto>> GetUserById(int id)
//         {
//             var query = new GetUserById(id);
//             var user = await mediator.Send(query);
//             if (user == null)
//             {
//                 return NotFound();
//             }
//             return Ok(user);
//         }
//
//         [HttpPut("{id}")]
//         [SwaggerOperation(Summary = "Updates an existing user")]
//         [SwaggerResponse(204, "User updated successfully")]
//         [SwaggerResponse(404, "User not found")]
//         public async Task<IActionResult> UpdateUser(int id, UpdateUser command)
//         {
//             var result = await mediator.Send(new UpdateUser(id, command.Username, command.Email, command.PasswordHash));
//             if (result == null)
//             {
//                 return NotFound();
//             }
//             return NoContent();
//         }
//
//         [HttpDelete("{id}")]
//         [SwaggerOperation(Summary = "Deletes a user by ID")]
//         [SwaggerResponse(204, "User deleted successfully")]
//         [SwaggerResponse(404, "User not found")]
//         public async Task<IActionResult> DeleteUser(int id)
//         {
//             var command = new DeleteUser(id);
//             var result = await mediator.Send(command);
//             if (!result)
//             {
//                 return NotFound();
//             }
//             return NoContent();
//         }
//
//         [HttpGet]
//         [SwaggerOperation(Summary = "Gets all users")]
//         [SwaggerResponse(200, "Users retrieved successfully", typeof(List<UserDto>))]
//         public async Task<ActionResult<List<UserDto>>> GetAllUsers()
//         {
//             var query = new GetAllUsers();
//             var users = await mediator.Send(query);
//             return Ok(users);
//         }
//     }
//
// }
