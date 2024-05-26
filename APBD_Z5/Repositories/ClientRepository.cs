using System.Collections.Frozen;
using System.Data;
using APBD_Z5.Dto.Response.Trip;
using APBD_Z5.Exceptions.ClientException;
using APBD_Z5.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Z5.Repositories;

public interface IClientRepository
{
    public Task<int> DeleteById(int idClient);
    public Task<Client?> FindOneByPesel(string pesel);

    public Task<Client> Create(Client client);

    public Task<bool> IsClientAssignedToTrip(int idClient, int idTrip);

    public Task<ClientTrip> AddClientToTrip(ClientTrip clientTrip);
}

public class ClientRepository : IClientRepository
{
    private readonly SQLDBContext context;

    public ClientRepository(SQLDBContext context)
    {
        this.context = context;
    }

    public async Task<int> DeleteById(int idClient)
    {
        var isClientFound = await context.Clients.AnyAsync(row => row.IdClient == idClient);
        if (!isClientFound)
        {
            throw new ClientNotFoundException();
        }
        var isTripAssigned = await context.ClientTrips.AnyAsync(row => row.IdClient == idClient);

        if (isTripAssigned)
        {
            throw new TripAssignedToClientException();
        }
        
        return await context.Clients.Where(a => a.IdClient.Equals(idClient)).ExecuteDeleteAsync();
    }

    public async Task<Client?> FindOneByPesel(string pesel)
    {
        var client = await context.Clients.FirstOrDefaultAsync(row => row.Pesel == pesel);

        return client;
    }

    public async Task<bool> IsClientAssignedToTrip(int idClient, int idTrip)
    {
        var client = await context.ClientTrips.FirstOrDefaultAsync(row => row.IdClient == idClient && row.IdTrip == idTrip);

        return client != null;
    }

    public async Task<ClientTrip> AddClientToTrip(ClientTrip clientTrip)
    {
        context.ClientTrips.Add(clientTrip);
        await context.SaveChangesAsync();
        return clientTrip;
    }

    public async Task<Client> Create(Client client)
    {
        context.Clients.Add(client);
        await context.SaveChangesAsync();
        return client;
    }
}