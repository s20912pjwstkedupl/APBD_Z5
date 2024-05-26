namespace APBD_Z5.Dto.Response.Trip;

public class AddClientToTripResponseDto : AddClientToTripRequestDto
{
    public int IdTrip { get; set; }
    public string TripName { get; set; }
}