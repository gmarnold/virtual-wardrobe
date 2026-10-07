using VirtualWardrobe.Api.Domain;

namespace VirtualWardrobe.Api.Contracts;

public sealed record GarmentResponse(
    Guid Id, string Name, string Category, string Subtype, string? Brand,
    string? Size, string? Notes, string Status, DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt, string[] Colors, string[] Seasons,
    string[] Occasions, string[] StyleTags)
{
    public static GarmentResponse From(Garment garment) => new(
        garment.Id, garment.Name, garment.Category, garment.Subtype,
        garment.Brand, garment.Size, garment.Notes, garment.Status.ToString(),
        garment.CreatedAt, garment.UpdatedAt,
        garment.Colors.Select(x => x.Name).Order().ToArray(),
        garment.Seasons.Select(x => x.Name).Order().ToArray(),
        garment.Occasions.Select(x => x.Name).Order().ToArray(),
        garment.StyleTags.Select(x => x.Name).Order().ToArray());
}
