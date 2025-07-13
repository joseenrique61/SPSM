namespace UI.Data.ApiClient
{
	public class ApiClient : IApiClient
	{
		private readonly HttpClient _client;

		private readonly IHttpContextAccessor _httpContextAccessor;

		public ApiClient(HttpClient client, IHttpContextAccessor httpContextAccesor, IConfiguration configuration, ILogger<ApiClient> logger)
		{
			_client = client;

			var baseAddress = configuration["API:BaseAddress"];
			if (string.IsNullOrEmpty(baseAddress))
			{
				logger.LogError("Base address is not set.");
				throw new Exception("Base address is not set.");
			}

			_client.BaseAddress = new Uri(baseAddress);
			_httpContextAccessor = httpContextAccesor;

			SetToken(_httpContextAccessor.HttpContext!.Session.GetString("JWToken") ?? "");
		}

		public void SetToken(string token)
		{
			_client.DefaultRequestHeaders.Authorization = new("Bearer", token);
			_httpContextAccessor.HttpContext!.Session.SetString("JWToken", token);
		}

		public async Task<HttpResponseMessage> Get(string route)
		{
			return await _client.GetAsync(route);
		}

		public async Task<HttpResponseMessage> Post<T>(string route, T data)
		{
			return await _client.PostAsJsonAsync(route, data);
		}
		public async Task<HttpResponseMessage> Put<T>(string route, T data)
		{
			return await _client.PutAsJsonAsync(route, data);
		}

		public async Task<HttpResponseMessage> Delete(string route)
		{
			return await _client.DeleteAsync(route);
		}
	}
}