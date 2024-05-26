using System.Data;
using APBD_Z5.Dto.Response.Trip;
using APBD_Z5.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Z5.Repositories;

public interface ITripRepository
{
    public Task<List<GetTripsResponseDto>> FindAllOrderedByDateDesc();
    public Task<Trip?> FindOneById(int idTrip);
}

public class TripRepository : ITripRepository
{
    private readonly SQLDBContext context;

    public TripRepository(SQLDBContext context)
    {
        this.context = context;
    }

    public async Task<List<GetTripsResponseDto>> FindAllOrderedByDateDesc()
    {
        return await context.Trips.Select(trip =>
        new GetTripsResponseDto 
        {
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople,
            Countries = trip.VCountryTrips.Select(countryTrip => new CountryResponseDto
            {
                Name = countryTrip.VCountry.Name
            }),
            Clients = trip.VClientTrips.Select(clientTrip => new ClientsResponseDto
            {
                FirstName = clientTrip.VClient.FirstName,
                LastName = clientTrip.VClient.LastName
            })
        }).OrderByDescending(trip => trip.DateFrom).ToListAsync();
    }

    public async Task<Trip?> FindOneById(int idTrip)
    {
        return await context.Trips.SingleOrDefaultAsync(trip => trip.IdTrip == idTrip);
    }
}