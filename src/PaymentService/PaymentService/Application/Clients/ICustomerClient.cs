using PaymentService.Application.DTOs;
using PaymentService.Domain.Models;

namespace PaymentService.Application.Clients;

public interface ICustomerClient
{
    public Task<ClientDTO> GetClientByUserId(int id);
}