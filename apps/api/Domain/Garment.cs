namespace VirtualWardrobe.Api.Domain;

public enum GarmentStatus { Active, Archived }

public sealed class Garment
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required string Subtype { get; set; }
    public string? Brand { get; set; }
    public string? Size { get; set; }
    public string? Notes { get; set; }
    public GarmentStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public List<Color> Colors { get; set; } = [];
    public List<Season> Seasons { get; set; } = [];
    public List<Occasion> Occasions { get; set; } = [];
    public List<StyleTag> StyleTags { get; set; } = [];
}

public sealed class Color { public int Id { get; set; } public required string Name { get; set; } }
public sealed class Season { public int Id { get; set; } public required string Name { get; set; } }
public sealed class Occasion { public int Id { get; set; } public required string Name { get; set; } }
public sealed class StyleTag { public int Id { get; set; } public required string Name { get; set; } }
