using System.Security.Cryptography;
using System.Text;
using UserService.Application;

namespace UserService.Infrastructure;

public class PasswordHasher : IPasswordHasher
{
	public string GenerateHash(string password)
	{
		var buffer = Encoding.UTF8.GetBytes(password);
		return Convert.ToHexString(SHA256.HashData(buffer));
	}

	public bool VerifyHash(string password, string hash)
	{
		return GenerateHash(password) == hash;
	}
}