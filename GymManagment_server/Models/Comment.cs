using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public int UserId { get; set; }

    public int GymId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    public int Rank { get; set; }

    [StringLength(150)]
    public string Description { get; set; } = null!;

    [ForeignKey("GymId")]
    [InverseProperty("Comments")]
    public virtual Gym Gym { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User User { get; set; } = null!;
}
