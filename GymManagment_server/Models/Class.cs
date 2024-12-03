using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

public partial class Class
{
    [StringLength(50)]
    public string? Name { get; set; }

    [Key]
    public int ClassId { get; set; }

    public int? Difficulty { get; set; }

    [StringLength(100)]
    public string Description { get; set; } = null!;

    public int GymId { get; set; }

    [ForeignKey("GymId")]
    [InverseProperty("Classes")]
    public virtual Gym Gym { get; set; } = null!;
}
