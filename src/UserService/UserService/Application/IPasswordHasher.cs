namespace UserService.Application;

public interface IPasswordHasher
{
    public string GenerateHash(string password);
    
    public bool VerifyHash(string password, string hash);
}