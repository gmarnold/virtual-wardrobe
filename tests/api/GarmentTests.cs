using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VirtualWardrobe.Api.Contracts;
using VirtualWardrobe.Api.Data;
using VirtualWardrobe.Api.Domain;

namespace VirtualWardrobe.Api.Tests;

public sealed class ApiFixture : IAsyncLifetime
{
    public WebApplicationFactory<Program> Factory { get; private set; } = null!;
    public async Task InitializeAsync()
    {
        var connection = Environment.GetEnvironmentVariable("TEST_DATABASE_CONNECTION")
            ?? throw new InvalidOperationException("Set TEST_DATABASE_CONNECTION to a dedicated PostgreSQL test database.");
        var parsed = new Npgsql.NpgsqlConnectionStringBuilder(connection);
        if (parsed.Database is null || !parsed.Database.EndsWith("_test", StringComparison.Ordinal))
            throw new InvalidOperationException("Test database name must end with _test.");
        Factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            builder.UseSetting("ConnectionStrings:Wardrobe", connection));
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<WardrobeDbContext>();
        await db.Database.MigrateAsync();
    }
    public Task DisposeAsync() { Factory.Dispose(); return Task.CompletedTask; }
}

public sealed class GarmentTests(ApiFixture fixture) : IClassFixture<ApiFixture>
{
    [Fact]
    public async Task Garments_return_seeded_records_and_relationships()
    {
        using var client = fixture.Factory.CreateClient();
        var response = await client.GetAsync("/api/garments");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        var garments = await response.Content.ReadFromJsonAsync<GarmentResponse[]>();
        Assert.NotNull(garments);
        Assert.Equal(10, garments.Length);
        Assert.Equal(garments.OrderBy(x => x.Name).Select(x => x.Name), garments.Select(x => x.Name));
        var cardigan = Assert.Single(garments, x => x.Name == "Lavender Cardigan");
        Assert.NotEqual(Guid.Empty, cardigan.Id);
        Assert.Equal("Active", cardigan.Status);
        Assert.Equal(["Lavender"], cardigan.Colors);
        Assert.Equal(["Fall", "Spring"], cardigan.Seasons);
        Assert.Equal(["Casual", "Work"], cardigan.Occasions);
        Assert.Equal(["Cozy", "Romantic"], cardigan.StyleTags);
        Assert.All(garments, item => Assert.Null(item.Size));
    }

    [Fact]
    public async Task Health_returns_safe_database_status()
    {
        using var client = fixture.Factory.CreateClient();
        var response = await client.GetAsync("/api/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("{\"status\":\"healthy\"}", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Health_returns_503_when_database_is_unavailable()
    {
        using var factory = fixture.Factory.WithWebHostBuilder(builder =>
            builder.UseSetting("ConnectionStrings:Wardrobe", "Host=localhost;Port=1;Database=unavailable_test;Username=test;Timeout=1"));
        using var client = factory.CreateClient();
        var response = await client.GetAsync("/api/health");
        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        Assert.Equal("{\"status\":\"unavailable\"}", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public void Mapping_serializes_camel_case_strings_arrays_and_nullable_metadata()
    {
        var garment = new Garment {
            Id = Guid.Parse("20000000-0000-4000-8000-000000000001"),
            Name = "Test blouse", Category = "Top", Subtype = "Blouse",
            Colors = [new Color { Id = 1, Name = "Cream" }],
            StyleTags = [new StyleTag { Id = 1, Name = "Romantic" }],
            CreatedAt = DateTimeOffset.Parse("2026-01-01T00:00:00Z"),
            UpdatedAt = DateTimeOffset.Parse("2026-01-01T00:00:00Z")
        };
        var json = JsonSerializer.SerializeToElement(GarmentResponse.From(garment), new JsonSerializerOptions(JsonSerializerDefaults.Web));
        Assert.Equal(garment.Id.ToString(), json.GetProperty("id").GetString());
        Assert.Equal("Active", json.GetProperty("status").GetString());
        Assert.Equal("Cream", json.GetProperty("colors")[0].GetString());
        Assert.Equal("Romantic", json.GetProperty("styleTags")[0].GetString());
        Assert.Equal(JsonValueKind.Null, json.GetProperty("size").ValueKind);
        Assert.Equal(0, json.GetProperty("occasions").GetArrayLength());
    }
}
