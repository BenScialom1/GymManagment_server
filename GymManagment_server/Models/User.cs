using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

[Index("Email", Name = "UQ__Users__A9D1053462655B06", IsUnique = true)]
public partial class User
{
    [StringLength(50)]
    public string? Username { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    public string? Password { get; set; }

    [Key]
    public int Id { get; set; }

    public DateOnly? BirthDate { get; set; }

    [StringLength(100)]
    public string Address { get; set; } = null!;

    [StringLength(50)]
    public string Difficulty { get; set; } = null!;

    public int? GenderId { get; set; }

    public bool IsManager { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("GymManagerNavigation")]
    public virtual ICollection<Gym> Gyms { get; set; } = new List<Gym>();
}
