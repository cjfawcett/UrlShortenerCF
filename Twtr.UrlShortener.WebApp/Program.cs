using Microsoft.EntityFrameworkCore;
using Twtr.Domain.UrlGeneration;
using Twtr.UrlService;
using Twtr.UrlShortener.Persistence;
using Twtr.WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContextFactory<UrlShortenerContext>();
builder.Services.AddTransient<IUrlShortener, UrlShortenerService>();
builder.Services.AddTransient<IShortUrlGenerator, ShortUrlGenerator>();


var app = builder.Build();

app.SeedDB();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
