using Intex2.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
<<<<<<< HEAD
using Microsoft.AspNetCore.Builder;
=======
>>>>>>> ccfd069ddd533c57dadc2d9694e01390a5daac4c
using Microsoft.AspNetCore.Mvc;  // For UnauthorizedObjectResult
using Microsoft.AspNetCore.Http;


{
    var apiKey = Environment.GetEnvironmentVariable("API_KEY");
    // Use the apiKey in some configuration or service setup
}


<<<<<<< HEAD



=======
>>>>>>> ccfd069ddd533c57dadc2d9694e01390a5daac4c

var staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
Console.WriteLine($"Static file path: {staticFilePath}");

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = staticFilePath
});

// Set the absolute path for the web root (important for static files)
builder.WebHost.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MoviesConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173", "https://nice-coast-08fe8e21e.6.azurestaticapps.net") // Allow multiple frontends
            .AllowAnyMethod()   // Allow any HTTP methods
            .AllowAnyHeader()); // Allow any headers
});


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 12;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    
    

    // Lockout settings (optional)
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); // <-- includes support for JWT, 2FA, etc.

async Task SeedRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });





var app = builder.Build();




// Secrets enrinrment ting
// Test endpoint to check environment variable
app.MapGet("/test-api-key", (HttpContext context) =>
{
    // Access the environment variable
    var apiKey = Environment.GetEnvironmentVariable("API_KEY");

    // Return it in the response to confirm if it was loaded correctly
    return Results.Ok(new { apiKey });
});






// Seed roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRoles(services);
}

// Apply the CORS policy
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
// Ensure that HTTP requests are redirected to HTTPS
=======
>>>>>>> ccfd069ddd533c57dadc2d9694e01390a5daac4c

app.UseHttpsRedirection();

//app.UseAuthentication(); // MUST come before UseAuthorization
//app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles(); // Serve static files

app.Run();

public class ApiService
{
    public void MakeApiRequest()
    {
        var apiKey = Environment.GetEnvironmentVariable("API_KEY");
        // Use the API key for API calls or authentication
    }
}