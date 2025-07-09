using System.Net.Http.Headers;

namespace PaymentService.Infrastructure.Clients;

public class AuthenticationPropagationHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization;

        if (!string.IsNullOrEmpty(authHeader))
        {
            // Propagates the same authentication header received
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
