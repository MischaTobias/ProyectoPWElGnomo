using System;
using System.Collections.Generic;

namespace ElGnomoModels.ViewModels;

public partial class RoleView
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<UserView> Users { get; } = new List<UserView>();
}
