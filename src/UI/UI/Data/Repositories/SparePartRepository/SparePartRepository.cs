using UI.Data.ApiClient;
using UI.Models;

namespace UI.Data.Repositories.SparePartRepository
{
    public class SparePartRepository(IApiClient client) : ISparePartRepository
    {
        public async Task<List<SparePart>> GetAll()
        {
            HttpResponseMessage response = await client.Get("search/product/all");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<List<SparePart>>())!;
            }
            return [];
        }

        public async Task<SparePart?> GetById(int id)
        {
            HttpResponseMessage response = await client.Get($"search/product/id/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SparePart>();
            }
            return null;
        }

        public async Task<List<SparePart>?> GetByCategory(string categoryName)
        {
            HttpResponseMessage response = await client.Get($"search/product/category/{categoryName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<SparePart>>();
            }
            return null;
        }

        public async Task<bool> Create(SparePart sparePart)
        {
            HttpResponseMessage response = await client.Post("inventory/product/create/", sparePart);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(SparePart sparePart)
        {
            HttpResponseMessage response = await client.Put("inventory/product/update/", sparePart);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            HttpResponseMessage response = await client.Delete($"inventory/product/delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}