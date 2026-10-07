using Microsoft.EntityFrameworkCore;
using VirtualWardrobe.Api.Domain;

namespace VirtualWardrobe.Api.Data;

public sealed class WardrobeDbContext(DbContextOptions<WardrobeDbContext> options) : DbContext(options)
{
    public DbSet<Garment> Garments => Set<Garment>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        var garment = model.Entity<Garment>();
        garment.Property(x => x.Name).HasMaxLength(160);
        garment.Property(x => x.Category).HasMaxLength(60);
        garment.Property(x => x.Subtype).HasMaxLength(80);
        garment.Property(x => x.Brand).HasMaxLength(120);
        garment.Property(x => x.Size).HasMaxLength(40);
        garment.Property(x => x.Notes).HasMaxLength(2000);
        garment.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        garment.HasIndex(x => x.Category);
        model.Entity<Color>().Property(x => x.Name).HasMaxLength(60);
        model.Entity<Color>().HasIndex(x => x.Name).IsUnique();
        garment.HasMany(x => x.Colors).WithMany().UsingEntity<Dictionary<string, object>>(
            "GarmentColor",
            right => right.HasOne<Color>().WithMany().HasForeignKey("ColorId"),
            left => left.HasOne<Garment>().WithMany().HasForeignKey("GarmentId"));
        model.Entity<Season>().Property(x => x.Name).HasMaxLength(60);
        model.Entity<Season>().HasIndex(x => x.Name).IsUnique();
        garment.HasMany(x => x.Seasons).WithMany().UsingEntity<Dictionary<string, object>>(
            "GarmentSeason",
            right => right.HasOne<Season>().WithMany().HasForeignKey("SeasonId"),
            left => left.HasOne<Garment>().WithMany().HasForeignKey("GarmentId"));
        model.Entity<Occasion>().Property(x => x.Name).HasMaxLength(60);
        model.Entity<Occasion>().HasIndex(x => x.Name).IsUnique();
        garment.HasMany(x => x.Occasions).WithMany().UsingEntity<Dictionary<string, object>>(
            "GarmentOccasion",
            right => right.HasOne<Occasion>().WithMany().HasForeignKey("OccasionId"),
            left => left.HasOne<Garment>().WithMany().HasForeignKey("GarmentId"));
        model.Entity<StyleTag>().Property(x => x.Name).HasMaxLength(60);
        model.Entity<StyleTag>().HasIndex(x => x.Name).IsUnique();
        garment.HasMany(x => x.StyleTags).WithMany().UsingEntity<Dictionary<string, object>>(
            "GarmentStyleTag",
            right => right.HasOne<StyleTag>().WithMany().HasForeignKey("StyleTagId"),
            left => left.HasOne<Garment>().WithMany().HasForeignKey("GarmentId"));
        DemoData.Configure(model);
    }
}
