using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

[Table("GYMS")]
public partial class Gym
{
    [StringLength(100)]
    public string? Name { get; set; }

    [Key]
    public int GymId { get; set; }

    public int? Level { get; set; }
}
