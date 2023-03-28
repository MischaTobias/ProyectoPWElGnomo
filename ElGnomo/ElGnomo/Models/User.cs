using System;
using System.Collections.Generic;

namespace ElGnomo.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
