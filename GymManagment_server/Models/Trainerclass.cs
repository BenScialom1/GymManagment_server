using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

[Table("TRAINERCLASSES")]
public partial class Trainerclass
{
    [Key]
    public int AssignmentId { get; set; }

    [StringLength(100)]
    public string? Schedule { get; set; }

    public int? TrainerId { get; set; }

    public int? ClassId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Trainerclasses")]
    public virtual Class? Class { get; set; }

    [ForeignKey("TrainerId")]
    [InverseProperty("Trainerclasses")]
    public virtual Trainer? Trainer { get; set; }
}
