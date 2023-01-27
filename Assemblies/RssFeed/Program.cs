using Microsoft.EntityFrameworkCore;
using RssFeed.Clients.Implementations;
using RssFeed.Clients.Interfaces;
using RssFeed.Services.Implementations;
using RssFeed.Services.Interfaces;
using RssFeedAPI.DataAccessLayer.Contexts;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryImplementations;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Host.UseSerilog()
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration["ConnectonStrings:DefaultConnection"],
    x => x.MigrationsAssembly(typeof(Context).Assembly.GetName().Name)));
builder.Services.AddTransient<IFeedRepository, FeedRepository>();
builder.Services.AddTransient<IFeedService, FeedService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IRssFeedClient, RssFeedClient>();


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
