using System.ComponentModel.DataAnnotations;

namespace VetTail.Web.Models.Auth;

public class LoginVM
{
    [Required]
    [MinLength(3)]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
