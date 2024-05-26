using System.Globalization;
using APBD_Z5.Dto.Response.Trip;
using APBD_Z5.Exceptions.ClientException;
using APBD_Z5.Models;
using APBD_Z5.Repositories;

namespace APBD_Z5.Services;

public interface ITripsService
{
    public Task<List<GetTripsResponseDto>> GetTrips();
    public Task<AddClientToTripResponseDto> AssignClientToTrip(int idTrip, AddClientToTripRequestDto dto);
}

public class TripsService : ITripsService
{
    private ITripRepository _tripRepository;
    private IClientRepository _clientRepository;

    public TripsService(ITripRepository tripRepository, IClientRepository clientRepository)
    {
        _tripRepository = tripRepository;
        _clientRepository = clientRepository;
    }
    public async Task<List<GetTripsResponseDto>> GetTrips()
    {
        return await _tripRepository.FindAllOrderedByDateDesc();
    }


    public async Task<AddClientToTripResponseDto> AssignClientToTrip(int idTrip, AddClientToTripRequestDto dto)
    {
        var trip = await _tripRepository.FindOneById(idTrip);
        if (trip == null || trip.IdTrip != idTrip)
        {
            throw new TripDoesNotExistException();
        }
        
        var client = await _clientRepository.FindOneByPesel(dto.Pesel);

        if (client == null || client.Pesel != dto.Pesel)
        {
            client = await _clientRepository.Create(new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            });
        }

        var isAssignedToTrip = await _clientRepository.IsClientAssignedToTrip(client.IdClient, idTrip);

        if (isAssignedToTrip)
        {
            throw new TripAssignedToClientException();
        }

        DateTime.TryParseExact(dto.PaymentDate, "M/dd/yyyy", null, DateTimeStyles.None, out DateTime parsedDate);
        
        await _clientRepository.AddClientToTrip(new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            PaymentDate = parsedDate,
            RegisteredAt = DateTime.Now
        });
        
        return new AddClientToTripResponseDto
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel,
            IdTrip = trip.IdTrip,
            TripName = trip.Name,
            PaymentDate = dto.PaymentDate
        };
    }
}