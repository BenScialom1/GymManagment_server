using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

public partial class Gym
{
    [StringLength(50)]
    public string? Name { get; set; }

    [Key]
    public int GymId { get; set; }

    public int? Level { get; set; }

    [StringLength(100)]
    public string Address { get; set; } = null!;

    public int Price { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    public int? GymManager { get; set; }

    [InverseProperty("Gym")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    [InverseProperty("Gym")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("GymManager")]
    [InverseProperty("Gyms")]
    public virtual User? GymManagerNavigation { get; set; }

    [InverseProperty("Gym")]
    public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
}
