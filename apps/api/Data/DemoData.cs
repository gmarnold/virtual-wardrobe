using Microsoft.EntityFrameworkCore;
using VirtualWardrobe.Api.Domain;

namespace VirtualWardrobe.Api.Data;

// Fixed IDs and timestamps make fixture migrations deterministic.
internal static class DemoData
{
    public static void Configure(ModelBuilder model)
    {
        model.Entity<Color>().HasData(
            new Color { Id = 1, Name = "Lavender" },
            new Color { Id = 2, Name = "Cream" },
            new Color { Id = 3, Name = "Brown" },
            new Color { Id = 4, Name = "Blue" },
            new Color { Id = 5, Name = "Mauve" },
            new Color { Id = 6, Name = "Sage" },
            new Color { Id = 7, Name = "Black" },
            new Color { Id = 8, Name = "Burgundy" },
            new Color { Id = 9, Name = "Pink" });
        model.Entity<Season>().HasData(
            new Season { Id = 1, Name = "Spring" },
            new Season { Id = 2, Name = "Summer" },
            new Season { Id = 3, Name = "Fall" },
            new Season { Id = 4, Name = "Winter" });
        model.Entity<Occasion>().HasData(
            new Occasion { Id = 1, Name = "Casual" },
            new Occasion { Id = 2, Name = "Work" },
            new Occasion { Id = 3, Name = "Evening" },
            new Occasion { Id = 4, Name = "Weekend" });
        model.Entity<StyleTag>().HasData(
            new StyleTag { Id = 1, Name = "Romantic" },
            new StyleTag { Id = 2, Name = "Cozy" },
            new StyleTag { Id = 3, Name = "Classic" },
            new StyleTag { Id = 4, Name = "Vintage" },
            new StyleTag { Id = 5, Name = "Minimal" },
            new StyleTag { Id = 6, Name = "Playful" });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000001"), Name = "Lavender Cardigan", Category = "Top", Subtype = "Cardigan",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000001"), ColorId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000001"), SeasonId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000001"), SeasonId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000001"), OccasionId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000001"), OccasionId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000001"), StyleTagId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000001"), StyleTagId = 2 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000002"), Name = "Cream Lace Blouse", Category = "Top", Subtype = "Blouse",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000002"), ColorId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000002"), SeasonId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000002"), SeasonId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000002"), OccasionId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000002"), OccasionId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000002"), StyleTagId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000002"), StyleTagId = 4 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000003"), Name = "Brown Flare Trousers", Category = "Bottom", Subtype = "Trousers",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000003"), ColorId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000003"), SeasonId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000003"), SeasonId = 4 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000003"), OccasionId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000003"), OccasionId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000003"), StyleTagId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000003"), StyleTagId = 4 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000004"), Name = "Blue Midi Skirt", Category = "Bottom", Subtype = "Skirt",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000004"), ColorId = 4 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000004"), SeasonId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000004"), SeasonId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000004"), OccasionId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000004"), OccasionId = 4 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000004"), StyleTagId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000004"), StyleTagId = 6 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000005"), Name = "Mauve Button-Up", Category = "Top", Subtype = "Shirt",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000005"), ColorId = 5 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000005"), SeasonId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000005"), SeasonId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000005"), OccasionId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000005"), StyleTagId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000005"), StyleTagId = 5 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000006"), Name = "Sage Wrap Dress", Category = "Dress", Subtype = "Wrap dress",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000006"), ColorId = 6 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000006"), SeasonId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000006"), SeasonId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000006"), OccasionId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000006"), OccasionId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000006"), StyleTagId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000006"), StyleTagId = 5 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000007"), Name = "Black Ankle Boots", Category = "Shoes", Subtype = "Boots",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000007"), ColorId = 7 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000007"), SeasonId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000007"), SeasonId = 4 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000007"), OccasionId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000007"), OccasionId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000007"), StyleTagId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000007"), StyleTagId = 5 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000008"), Name = "Cream Sneakers", Category = "Shoes", Subtype = "Sneakers",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000008"), ColorId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000008"), SeasonId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000008"), SeasonId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000008"), SeasonId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000008"), OccasionId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000008"), OccasionId = 4 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000008"), StyleTagId = 5 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000009"), Name = "Burgundy Coat", Category = "Outerwear", Subtype = "Coat",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000009"), ColorId = 8 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000009"), SeasonId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000009"), SeasonId = 4 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000009"), OccasionId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000009"), OccasionId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000009"), StyleTagId = 3 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000009"), StyleTagId = 4 });
        model.Entity<Garment>().HasData(new
        {
            Id = Guid.Parse("10000000-0000-4000-8000-000000000010"), Name = "Floral Sundress", Category = "Dress", Subtype = "Sundress",
            Brand = "Demo Atelier", Size = (string?)null, Notes = "Fictional demo garment.",
            Status = GarmentStatus.Active,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
            UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), ColorId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentColor").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), ColorId = 9 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), SeasonId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentSeason").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), SeasonId = 2 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), OccasionId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentOccasion").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), OccasionId = 4 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), StyleTagId = 1 });
        model.SharedTypeEntity<Dictionary<string, object>>("GarmentStyleTag").HasData(new { GarmentId = Guid.Parse("10000000-0000-4000-8000-000000000010"), StyleTagId = 6 });
    }
}
