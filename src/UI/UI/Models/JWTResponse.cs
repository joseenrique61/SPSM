namespace UI.Models
{
	public class JWTResponse
	{
		public string Token { get; set; }

		public string Email { get; set; }

		public string Role { get; set; }

		public int ClientId { get; set; }
	}
}