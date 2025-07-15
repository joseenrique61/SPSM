using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Models;
using UserService.Domain.Repositories;

namespace UserService.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController(IUserRepository userRepository, ILogger<UserController> logger) : Controller
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] Client? client)
    {
        if (client?.User == null || string.IsNullOrEmpty(client.User.Email) || string.IsNullOrEmpty(client.User.Password))
        {
            return Unauthorized("User is empty.");
        }

        try
        {
            var response = await userRepository.RegisterUserAsync(client);
            return Ok(response);
        }
        catch
        {
            return Conflict("The user already exists.");
        }
    }

    [HttpPost]
    [Route("register_admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] User user)
    {
        try
        {
            var response = await userRepository.RegisterAdminAsync(user);
            return Ok(response);
        }
        catch
        {
            return Conflict("The user already exists.");
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] User? user)
    {
        if (user?.Password == null)
        {
            return Unauthorized("User is empty.");
        }

        try
        {
            var response = await userRepository.LoginAsync(user);
            return Ok(response);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Unauthorized("The user and/or password are incorrect.");
        }
    }
}