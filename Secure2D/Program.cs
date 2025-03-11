using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Secure2D.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["SqlConnectionString"];

builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 10;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddDapperStores(options => options.ConnectionString = connectionString);

// Add services to the container.

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IEnvironment2DRepository, Environment2DRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme)
    .Configure(options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromMinutes(60); // Token verloopt na 60 minuten
    });

var app = builder.Build();

app.MapGet("/", () => "Hello world, the API is up ");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGroup("/account")
    .MapIdentityApi<IdentityUser>();

app.MapPost("/account/logout",
    async (SignInManager<IdentityUser> signinManager,
    [FromBody] object empty) =>
    {
        if (empty == null)
        {
            await signinManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    })
    .RequireAuthorization();


app.MapControllers()
    .RequireAuthorization();

app.Run();
