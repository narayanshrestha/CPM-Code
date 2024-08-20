using BlazorWasm.Client.Models;

namespace BlazorWasm.Client.Services
{
    public interface IAuthenticationService
    {
        event Action<string?>? LoginChange;
        ValueTask<string> GetJwtAsync();
        Task<DateTime> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<bool> RegisterAsync(RegistrationModel model);
    }
}
