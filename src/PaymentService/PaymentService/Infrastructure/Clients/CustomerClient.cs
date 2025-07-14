using PaymentService.Application.Clients;
using PaymentService.Application.DTOs;
using PaymentService.Domain.Models;
using System.Net.Http;

namespace PaymentService.Infrastructure.Clients
{
    public class CustomerClient(HttpClient httpClient) : ICustomerClient
    {
        public async Task<ClientDTO> GetClientByUserId(int id)
        {
            var client = await httpClient.GetFromJsonAsync<ClientDTO>($"id/{id}");

            if (client == null)
                throw new Exception("There isn't a customer with that UserId");

            return client;
        }
    }
}
