using APBD_Z5.Repositories;

namespace APBD_Z5.Services;


public interface IClientsService
{
    public Task<int> DeleteClientById(int idClient);
}
public class ClientsService : IClientsService
{
    private IClientRepository _clientRepository;

    public ClientsService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<int> DeleteClientById(int idClient)
    {
        await _clientRepository.DeleteById(idClient);
        return 1;
    }
}