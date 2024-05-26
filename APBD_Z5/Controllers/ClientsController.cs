using APBD_Z5.Dto.Response.Trip;
using APBD_Z5.Exceptions.ClientException;
using APBD_Z5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Z5.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;
    
    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
    
    [HttpDelete("{idClient}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteById([FromRoute] int idClient)
    {
        try
        {
            await _clientsService.DeleteClientById(idClient);
            return NoContent();
        }
        catch (TripAssignedToClientException)
        {
            return BadRequest("Cannot remove client with assigned trip");
        }
        catch (ClientNotFoundException)
        {
            return BadRequest("Client was not found");
        }
    }
}