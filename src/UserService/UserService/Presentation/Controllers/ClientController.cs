using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Repositories;

namespace UserService.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ClientController(IClientRepository clientRepository, ILogger<UserController> logger) : Controller
{
    [HttpGet]
    [Route("id/{id}")]
    public async Task<IActionResult> GetClientByUserId(int id)
    {
        try
        {
            var response = await clientRepository.GetClientByUserId(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return BadRequest("The UserId is incorrect.");
        }
    }
}