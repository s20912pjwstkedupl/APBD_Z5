namespace APBD_Z5.Dto.Response.Trip;

public class GetTripsResponseDto
{
    public GetTripsResponseDto()
    {
        Countries = new List<CountryResponseDto>();
        Clients = new List<ClientsResponseDto>();
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public IEnumerable<CountryResponseDto> Countries { get; set; }
    public IEnumerable<ClientsResponseDto> Clients { get; set; }
}

public class CountryResponseDto
{
    public string Name { get; set; }
}

public class ClientsResponseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}