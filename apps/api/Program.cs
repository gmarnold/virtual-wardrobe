using Microsoft.EntityFrameworkCore;
using VirtualWardrobe.Api.Contracts;
using VirtualWardrobe.Api.Data;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("Wardrobe")
    ?? throw new InvalidOperationException("Configure ConnectionStrings__Wardrobe.");
builder.Services.AddDbContext<WardrobeDbContext>(options => options.UseNpgsql(connection));
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    if (origins.Length > 0)
        policy.WithOrigins(origins).WithMethods("GET").WithHeaders("Content-Type");
}));
var app = builder.Build();
app.UseExceptionHandler();
app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/api/garments", async (WardrobeDbContext db, CancellationToken cancellationToken) =>
{
    var garments = await db.Garments.AsNoTracking()
        .Include(x => x.Colors).Include(x => x.Seasons)
        .Include(x => x.Occasions).Include(x => x.StyleTags)
        .AsSplitQuery().OrderBy(x => x.Name).ToListAsync(cancellationToken);
    return Results.Ok(garments.Select(GarmentResponse.From).ToArray());
}).WithName("GetGarments").WithSummary("List demo wardrobe garments")
  .Produces<GarmentResponse[]>();
app.MapGet("/api/health", async (WardrobeDbContext db, CancellationToken cancellationToken) =>
{
    try
    {
        return await db.Database.CanConnectAsync(cancellationToken)
            ? Results.Ok(new HealthResponse("healthy"))
            : Results.Json(new HealthResponse("unavailable"), statusCode: 503);
    }
    catch (Exception) when (!cancellationToken.IsCancellationRequested)
    {
        return Results.Json(new HealthResponse("unavailable"), statusCode: 503);
    }
}).WithName("GetHealth").WithSummary("Check API and database connectivity")
  .Produces<HealthResponse>().Produces<HealthResponse>(503);
app.Run();

public sealed record HealthResponse(string Status);
public partial class Program;
