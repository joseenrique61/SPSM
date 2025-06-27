using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Models;
using UserService.Domain.Repositories;

namespace UserService.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController(IUserRepository userRepository) : Controller
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] Client? client)
    {
        if (client?.User == null || string.IsNullOrEmpty(client.User.Email) || string.IsNullOrEmpty(client.User.Password))
        {
            return Unauthorized("User is empty.");
        }

        return await userRepository.RegisterUserAsync(client) ? Ok() : Conflict("The user already exists.");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] User? user)
    {
        if (user?.Password == null)
        {
            return Unauthorized("User is empty.");
        }

        return await userRepository.LoginAsync(user) ? Ok() : Unauthorized("The user and/or password are incorrect.");
    }
}