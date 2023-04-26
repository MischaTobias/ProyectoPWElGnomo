using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ElGnomoModels.ViewModels;

public partial class RoleUsersView
{
    public int Id { get; set; }
    [Display(Name = "Usuario")]
    public UserView User { get; set; } = default!;
    [Display(Name = "Rol")]
    public RoleView Role { get; set; } = default!;
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public List<SelectListItem> Users { get; set; } = default!;
    public List<SelectListItem> Roles { get; set; } = default!;
}
