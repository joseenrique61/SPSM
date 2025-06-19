namespace NotificationService.Application.Interfaces
{
    public interface INotificationProvider  : IServiceProvider
    {
        string ProviderType { get; }
        Task<bool> SendAsync(string recipient, string subject, string body);
    }
}
