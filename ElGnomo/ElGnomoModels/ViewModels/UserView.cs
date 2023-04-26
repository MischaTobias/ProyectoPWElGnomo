using System.ComponentModel.DataAnnotations;

namespace ElGnomoModels.ViewModels;

public partial class UserView
{
    public int Id { get; set; }
    [Display(Name = "Nombre")]
    public string FirstName { get; set; } = default!;
    [Display(Name = "Apellido")]
    public string LastName { get; set; } = default!;
    [Display(Name = "Correo electrónico")]
    public string Email { get; set; } = default!;
    [Display(Name = "Contraseña")]
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}
