using UI.Data.ApiClient;
using UI.Models;

namespace UI.Data.Repositories.ClientRepository
{
    public class ClientRepository(IApiClient apiClient, IHttpContextAccessor contextAccessor) : IClientRepository
    {
        public async Task<bool> Login(string email, string password)
        {
            var response = await apiClient.Post("user/login", new User()
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var token = await response.Content.ReadFromJsonAsync<JWTResponse>();
            if (token == null)
            {
                return false;
            }

            apiClient.SetToken(token.Token);
            contextAccessor.HttpContext!.Session.SetString("Email", token.Email);
            contextAccessor.HttpContext!.Session.SetString("Role", token.Role);
            contextAccessor.HttpContext!.Session.SetInt32("ClientId", token.ClientId);

            return true;
        }

        public async Task<bool> Register(Client client)
        {
            var response = await apiClient.Post("user/register", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<Client?> GetById(int id)
        {
            var response = await apiClient.Get($"client/full/id/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return (await response.Content.ReadFromJsonAsync<Client>())!;
        }
    }
}