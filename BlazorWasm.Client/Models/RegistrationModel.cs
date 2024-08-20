using System.ComponentModel.DataAnnotations;

namespace BlazorWasm.Client.Models
{
    public class RegistrationModel
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}
