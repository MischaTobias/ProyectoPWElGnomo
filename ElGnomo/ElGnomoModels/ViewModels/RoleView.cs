using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElGnomoModels.ViewModels;

public partial class RoleView
{
    public int Id { get; set; }

    [Display(Name="Nombre")]
    public string? Name { get; set; }

    [Display(Name="Descripción")]
    public string? Description { get; set; }

    public virtual ICollection<UserView> Users { get; } = new List<UserView>();
}
