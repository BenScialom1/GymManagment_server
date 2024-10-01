using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

[Table("CLASSES")]
public partial class Class
{
    [StringLength(100)]
    public string? Name { get; set; }

    [Key]
    public int ClassId { get; set; }

    [Column("DIFFICULTY")]
    public int? Difficulty { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<Trainerclass> Trainerclasses { get; set; } = new List<Trainerclass>();
}
