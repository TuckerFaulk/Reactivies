// Create a WebApplicationBuilder — entry point for setting up services and config

using Application.Activities.Queries;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// =============================
// Add services to the container
// =============================

// Adds support for [ApiController]s and routing — enables attribute-based routing for your controllers
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
	opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

// =============================
// Build the WebApplication
// =============================
var app = builder.Build();


// =======================================
// Configure the HTTP request pipeline
// =======================================

// Configure CORS (Cross-Origin Resource Sharing) policy
// This allows the API to receive requests from different origins (domains)
app.UseCors(x => x
    .AllowAnyHeader()   // Allows requests with any header
    .AllowAnyMethod()   // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
    .WithOrigins(       // Restricts which domains can access the API:
        "http://localhost:3000",  // Allow requests from local React dev server (HTTP)
        "https://localhost:3000"  // Allow requests from local React dev server (HTTPS)
    )
);

// Maps your API controllers' routes into the app
// This enables the endpoints you define with [Route] and [HttpGet], etc.
app.MapControllers();


// =======================================
// Migrate and seed the database
// =======================================

// Create a scoped service provider — this ensures we get services with proper DI scope (like DbContext)
using var scope = app.Services.CreateScope();

// Access the scoped service provider
var services = scope.ServiceProvider;

try
{
	// Resolve the AppDbContext from DI
	var context = services.GetRequiredService<AppDbContext>();

	// Apply any pending EF Core migrations to the database at startup
	await context.Database.MigrateAsync();

	// Seed initial data into the database (if needed)
	await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
	// Resolve a logger for this Program class
	var logger = services.GetRequiredService<ILogger<Program>>();

	// Log any errors that occur during migration or seeding
	logger.LogError(ex, "An error occurred while migrating or seeding the database.");
}

// Starts the web server and begins listening for requests
app.Run();