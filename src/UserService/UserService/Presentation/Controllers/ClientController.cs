using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Repositories;

namespace UserService.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ClientController(IClientRepository clientRepository, ILogger<UserController> logger) : Controller
{
    [HttpPost]
    [Route("client/{id}")]
    public async Task<IActionResult> GetClient(int id)
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