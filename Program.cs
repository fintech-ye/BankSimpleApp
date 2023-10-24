using Microsoft.EntityFrameworkCore;
using BankSimpleApp.Data;
using Microsoft.Extensions.Options;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
builder.Services.AddRazorPages();

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
Console.WriteLine(environmentName);

if(environmentName != null && environmentName.Equals("Development")){
    builder.Services.AddDbContext<MakeenContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myDB")));
}
else{
    string connectionString = builder.Configuration.GetConnectionString("MyDB").ToString();

    connectionString = Environment.ExpandEnvironmentVariables(connectionString);

    Console.WriteLine(connectionString);

    builder.Services.AddDbContext<MakeenContext>(options =>
        options.UseSqlServer(connectionString));
}


var app = builder.Build();

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
