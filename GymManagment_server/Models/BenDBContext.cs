using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GymManagment_server.Models;

public partial class BenDBContext : DbContext
{
    public BenDBContext()
    {
    }

    public BenDBContext(DbContextOptions<BenDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Gym> Gyms { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<Trainerclass> Trainerclasses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=GymManagment_server;User ID=TaskAdminLogin;Password=Petel123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__CLASSES__CB1927C01C67D1BA");
        });

        modelBuilder.Entity<Gym>(entity =>
        {
            entity.HasKey(e => e.GymId).HasName("PK__GYMS__1A3A7C96C473CFED");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__TRAINERS__366A1A7C9CA8F9D5");
        });

        modelBuilder.Entity<Trainerclass>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__TRAINERC__32499E775DDC01D1");

            entity.Property(e => e.AssignmentId).ValueGeneratedNever();

            entity.HasOne(d => d.Class).WithMany(p => p.Trainerclasses).HasConstraintName("FK__TRAINERCL__Class__2D27B809");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Trainerclasses).HasConstraintName("FK__TRAINERCL__Train__2C3393D0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Password).HasName("PK__Users__87909B14AC5ED311");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
