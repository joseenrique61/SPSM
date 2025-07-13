namespace UI.Utilities
{
	public interface IAuthenticator
	{
		public bool Authenticate(string expectedRole);
	}
}