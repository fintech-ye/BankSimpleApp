using Microsoft.EntityFrameworkCore;
using asp_simple.Data;
using Microsoft.Extensions.Options;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
builder.Services.AddRazorPages();

// builder.Services.AddDbContext<MakeenContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("makeenDB")));

Console.WriteLine(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"));

builder.Services.AddDbContext<MakeenContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// using var scope = app.Services.CreateScope();
// var db = scope.ServiceProvider.GetRequiredService<MakeenContext>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
