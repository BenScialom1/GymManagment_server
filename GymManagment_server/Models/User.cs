using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

public partial class User
{
    [StringLength(100)]
    public string? Username { get; set; }

    [Key]
    [StringLength(100)]
    public string Password { get; set; } = null!;

    public int? Age { get; set; }

    [StringLength(100)]
    public string? Gender { get; set; }
}
