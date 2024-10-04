using Microsoft.EntityFrameworkCore;
using TaggerApi.Models;
using Microsoft.Extensions.DependencyInjection;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using TaggerApi.Services.Authentication;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? cadena = builder.Configuration.GetConnectionString("DefaultConnection") ?? "otracadena";
builder.Services.AddControllers();

builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(cadena));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("firebase.json")
});

builder.Services.AddSingleton<IAuthenticationService,AuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
