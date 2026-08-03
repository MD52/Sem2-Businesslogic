namespace BuckingMachine.Infrastructure.Persistence;

using BuckingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class BuckingMachineDbContext(DbContextOptions<BuckingMachineDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.BuckingMachine> Machines => Set<Domain.Entities.BuckingMachine>();
    public DbSet<ParameterData> ParameterData => Set<ParameterData>();
    public DbSet<StatusData> StatusData => Set<StatusData>();
    public DbSet<MachineCycle> MachineCycles => Set<MachineCycle>();
    public DbSet<Alarm> Alarms => Set<Alarm>();
    public DbSet<Operator> Operators => Set<Operator>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.BuckingMachine>(entity =>
        {
            entity.HasKey(x => x.MachineId);
            entity.Property(x => x.Name).IsRequired();
            entity.Ignore(x => x.ParameterData);
            entity.Ignore(x => x.StatusData);
            entity.Ignore(x => x.ActiveAlarm);
        });

        modelBuilder.Entity<ParameterData>(entity =>
        {
            entity.HasKey(x => x.ParameterDataId);
            entity.HasOne<Domain.Entities.BuckingMachine>().WithMany().HasForeignKey(x => x.MachineId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<StatusData>(entity =>
        {
            entity.HasKey(x => x.StatusDataId);
            entity.HasOne<Domain.Entities.BuckingMachine>().WithMany().HasForeignKey(x => x.MachineId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<MachineCycle>(entity =>
        {
            entity.HasKey(x => x.CycleId);
            entity.Property(x => x.Name).IsRequired();
            entity.HasOne<Domain.Entities.BuckingMachine>().WithMany().HasForeignKey(x => x.MachineId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.ParameterData).WithMany().HasForeignKey(x => x.ParameterDataId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.StatusData).WithMany().HasForeignKey(x => x.StatusDataId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Alarm>(entity =>
        {
            entity.HasKey(x => x.AlarmId);
            entity.Property(x => x.Code).IsRequired();
            entity.Property(x => x.Message).IsRequired();
        });
        modelBuilder.Entity<Operator>().HasKey(x => x.OperatorId);
    }
}
