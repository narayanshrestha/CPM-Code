using System.Net.Http.Headers;
using System.Net;
using BlazorWasm.Client.Services;

namespace BlazorWasm.Client.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationHandler(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _authenticationService.GetJwtAsync();
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

            if (isToServer && !string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await base.SendAsync(request, cancellationToken);

            if (!string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized)
            {

                jwt = await _authenticationService.GetJwtAsync();

                if (isToServer && !string.IsNullOrEmpty(jwt))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                response = await base.SendAsync(request, cancellationToken);

            }

            return response;
        }
    }
}
