using System.Security.Cryptography;
using System.Text;

namespace UserService.Application;

public static class PasswordHasher
{
	public static string Hash(string password)
	{
		var buffer = Encoding.UTF8.GetBytes(password);
		return Convert.ToHexString(SHA256.HashData(buffer));
	}

	public static bool Verify(string password, string hash)
	{
		return Hash(password) == hash;
	}
}