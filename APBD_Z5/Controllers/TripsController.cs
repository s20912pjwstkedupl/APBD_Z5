using APBD_Z5.Dto.Response.Trip;
using APBD_Z5.Exceptions.ClientException;
using APBD_Z5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Z5.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripsService _tripsService;
    
    public TripsController(ITripsService tripsService)
    {
        _tripsService = tripsService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetTripsResponseDto>>> GetTripsAsync()
    {
        return Ok(await _tripsService.GetTrips());
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<ActionResult<AddClientToTripResponseDto>> RegisterClientToTripAsync([FromRoute] int idTrip, [FromBody] AddClientToTripRequestDto requestDto)
    {
        try
        {
            var result = await _tripsService.AssignClientToTrip(idTrip, requestDto);
            return Ok(result);
        }
        catch (TripAssignedToClientException)
        {
            return BadRequest("This trip is already assigned to this client");
        }
        catch (ClientNotFoundException)
        {
            return BadRequest("Client with provided id was not found");
        }
        catch (TripDoesNotExistException)
        {
            return BadRequest("Trip with provided id was not found");
        }
    }
}