using System.ComponentModel.DataAnnotations;

namespace BlazorWasm.Client.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }
    }
}
