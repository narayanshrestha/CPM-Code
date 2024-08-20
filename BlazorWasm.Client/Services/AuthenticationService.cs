using Blazored.SessionStorage;
using BlazorWasm.Client.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlazorWasm.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _factory;
        private ISessionStorageService _sessionStorageService;

        private const string JWT_KEY = nameof(JWT_KEY);
        private const string REFRESH_KEY = nameof(REFRESH_KEY);

        private string? _jwtCache;

        public event Action<string?>? LoginChange;

        public AuthenticationService(IHttpClientFactory factory, ISessionStorageService sessionStorageService)
        {
            _factory = factory;
            _sessionStorageService = sessionStorageService;
        }

        public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(JWT_KEY);

            return _jwtCache;
        }

        public async Task LogoutAsync()
        {
            var response = await _factory.CreateClient("ApiEndPoint").DeleteAsync("api/authentication/revoke");

            await _sessionStorageService.RemoveItemAsync(JWT_KEY);
            await _sessionStorageService.RemoveItemAsync(REFRESH_KEY);

            _jwtCache = null;

            await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

            LoginChange?.Invoke(null);
        }

        private static string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);

            return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }

        public async Task<DateTime> LoginAsync(LoginModel model)
        {
            var response = await _factory.CreateClient("ApiEndPoint").PostAsync("api/authentication/login",
                                                        JsonContent.Create(model));

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Login failed.");

            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (content == null)
                throw new InvalidDataException();

            await _sessionStorageService.SetItemAsync(JWT_KEY, content.JwtToken);
            //await _sessionStorageService.SetItemAsync(REFRESH_KEY, content.RefreshToken);

            LoginChange?.Invoke(GetUsername(content.JwtToken));

            return content.Expiration;
        }

        public async Task<bool> RegisterAsync(RegistrationModel model)
        {
            var response = await _factory.CreateClient("ApiEndPoint").PostAsync("api/authentication/register",
                                                        JsonContent.Create(model));

            if (!response.IsSuccessStatusCode)
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                throw new Exception(errMsg);

            }

            return true;
        }
    }
}
