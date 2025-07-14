using PaymentService.Application.DTOs;

namespace PaymentService.Application.Clients;

public interface ICustomerClient
{
    public Task<ClientDTO> GetClient(int id);
}

