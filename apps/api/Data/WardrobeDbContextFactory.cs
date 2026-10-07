using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VirtualWardrobe.Api.Data;

public sealed class WardrobeDbContextFactory : IDesignTimeDbContextFactory<WardrobeDbContext>
{
    public WardrobeDbContext CreateDbContext(string[] args)
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionStrings__Wardrobe")
            ?? throw new InvalidOperationException("Set ConnectionStrings__Wardrobe before running migrations.");
        return new WardrobeDbContext(new DbContextOptionsBuilder<WardrobeDbContext>()
            .UseNpgsql(connection).Options);
    }
}
