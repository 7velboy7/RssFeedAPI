using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RssFeed.Clients.Implementations;
using RssFeed.Clients.Interfaces;
using RssFeed.Services.Implementations;
using RssFeed.Services.Interfaces;
using RssFeedAPI.DataAccessLayer.Contexts;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryImplementations;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Host.UseSerilog()
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"],
    x => x.MigrationsAssembly(typeof(Context).Assembly.GetName().Name)));
builder.Services.AddTransient<IFeedRepository, FeedRepository>();
builder.Services.AddTransient<IFeedService, FeedService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<INewsRepository, NewsRepository>();
builder.Services.AddTransient<IRssFeedClient, RssFeedClient>();

builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
