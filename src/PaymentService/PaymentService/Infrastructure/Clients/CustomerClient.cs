using PaymentService.Application.Clients;
using PaymentService.Application.DTOs;

namespace PaymentService.Infrastructure.Clients
{
    public class CustomerClient(HttpClient httpClient) : ICustomerClient
    {
        public async Task<ClientDTO> GetClient(int id)
        {
            var client = await httpClient.GetFromJsonAsync<ClientDTO>($"id/{id}");

            if (client == null)
                throw new Exception("There isn't customer information related to this UserId");
                
            return client;
        }
    }
}
