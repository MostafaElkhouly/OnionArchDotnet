using Domain.Configration.EntitiesProperties;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Config.ConfigurationService;
using Presentation.Configration.Configrations;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://*:80");
//builder.WebHost.UseUrls("https://*:443");
// Add services to the container.

string AllowEveryThing = "AllowEveryThing";

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowEveryThing,
        builder =>
        {
            builder
           .WithOrigins("*",
               "http://localhost:4100",
               "http://localhost:4200"
           )
           .SetIsOriginAllowed(e => true)
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));
});

builder.Services.AddScopedRepository();
builder.Services.AddScopedService();
builder.Services.AddScopedAutoMapper();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddIdentity<Account, IdentityRole>(options =>
    {

    }).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(AllowEveryThing);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
