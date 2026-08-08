namespace BuckingMachine.Infrastructure.Persistence;

using BuckingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class BuckingMachineDbContext : DbContext
{
    public BuckingMachineDbContext(
        DbContextOptions<BuckingMachineDbContext> options)
        : base(options)
    {
    }

    public DbSet<ParameterData> ParameterData => Set<ParameterData>();

     protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ParameterData>(entity =>
        {
            entity.ToTable("ParameterData");

            entity.HasKey(parameter =>
                parameter.ParameterDataId);

            entity.Property(parameter =>
                    parameter.ParameterDataId)
                .ValueGeneratedOnAdd();

            entity.Property(parameter =>
                    parameter.RecordedAt)
                .IsRequired();

            entity.Property(parameter =>
                    parameter.MachineId)
                .IsRequired();

            entity.Property(parameter =>
                    parameter.AmountCycleMovements)
                .IsRequired();
        });
    }




}
