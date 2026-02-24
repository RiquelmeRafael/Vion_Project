using System.ComponentModel.DataAnnotations;

namespace Vion.Web.Models
{
    public class ForgotPasswordVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}

