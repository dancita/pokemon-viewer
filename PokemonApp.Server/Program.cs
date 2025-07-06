using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PokemonApp.Server.Authorization;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Middleware;
using PokemonApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);
var auth0Settings = builder.Configuration.GetRequiredSection("Auth0");
var authority = auth0Settings["Authority"];
var audience = auth0Settings["Audience"];

// Add services to the container.
builder.Services.AddHttpClient("httpClient");
builder.Services.AddScoped<IPokemonInfoService, PokemonInfoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:55481")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = authority;
    options.Audience = audience;
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("read:pokemon", policy => 
    policy.Requirements.Add(
        new HasScopeRequirement("read:pokemon", authority)
    ));

builder.Services.AddControllers();
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();