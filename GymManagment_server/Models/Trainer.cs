using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

[Table("TRAINERS")]
public partial class Trainer
{
    [StringLength(100)]
    public string? Name { get; set; }

    [Key]
    public int TrainerId { get; set; }

    public int? NumOfClasses { get; set; }

    [InverseProperty("Trainer")]
    public virtual ICollection<Trainerclass> Trainerclasses { get; set; } = new List<Trainerclass>();
}
