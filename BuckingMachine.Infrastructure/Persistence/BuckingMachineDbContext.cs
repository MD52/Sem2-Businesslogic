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
}
