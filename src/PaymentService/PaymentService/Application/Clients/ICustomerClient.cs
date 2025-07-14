using PaymentService.Application.DTOs;

namespace PaymentService.Application.Clients;

public interface ICostumerClient
{
    public Task<ClientDTO> GetClient(int id);
}
