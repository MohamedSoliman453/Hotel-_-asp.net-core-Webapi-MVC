using System.Net.Http.Headers;

public class JwtTokenHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Retrieve the JWT token from the secure storage (e.g., cookie)
        var jwtToken = ""; // Logic to retrieve token from cookie or session

        if (!string.IsNullOrWhiteSpace(jwtToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
