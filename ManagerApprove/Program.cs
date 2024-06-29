using Hangfire;
using Hangfire.SqlServer;
using ManagerApprove.Contracts;
using ManagerApprove.Models;
using ManagerApprove.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var mydb = builder.Configuration.GetConnectionString("MyDB");
var hangdb = builder.Configuration.GetConnectionString("HangfireDB");


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MiniProjDbContext>(options => 
    options.UseSqlServer(mydb));

builder.Services.AddScoped<ITimesheet, TimesheetService>();

builder.Services.AddHangfire(config =>
config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseDefaultTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(hangdb, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    })
);
builder.Services.AddTransient<HangfireService>();
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseHangfireDashboard();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
