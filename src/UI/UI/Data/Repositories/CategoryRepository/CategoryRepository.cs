using UI.Data.ApiClient;
using UI.Models;

namespace UI.Data.Repositories.CategoryRepository
{
	public class CategoryRepository(IApiClient client) : ICategoryRepository
	{
		public async Task<List<Category>?> GetAll()
		{
			HttpResponseMessage response = await client.Get("search/category/all");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<List<Category>>();
			}
			return null;
		}

		public async Task<Category?> GetById(int id)
		{
			HttpResponseMessage response = await client.Get($"search/category/id/{id}");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<Category>();
			}
			return null;
		}

		public async Task<Category?> GetByName(string name)
		{
			HttpResponseMessage response = await client.Get($"serach/category/name/{name}");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<Category>();
			}
			return null;
		}

		public async Task<bool> Create(Category category)
		{
			HttpResponseMessage response = await client.Post("inventory/category/create", category);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Update(Category category)
		{
			HttpResponseMessage response = await client.Put("inventory/category/update", category);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Delete(int id)
		{
			HttpResponseMessage response = await client.Delete($"inventory/category/delete/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}