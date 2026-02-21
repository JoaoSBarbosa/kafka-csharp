using Microsoft.AspNetCore.Mvc;
using Producer.Application.Users.DTOs;
using Producer.Application.Users.Services;

namespace Producer.Api.Controllers;

[ApiController]
[Route("v1/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        await _userService.AddAsync(request);
        return Accepted();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> FindById([FromRoute] Guid id)
    {
        var result = await _userService.FindByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var result = await _userService.GetAllAsync();
        return Ok(result);
    }
}