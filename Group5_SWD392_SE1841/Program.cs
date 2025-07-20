using Group5_SWD392_SE1841.Models;
using Group5_SWD392_SE1841.Repositories;
using Group5_SWD392_SE1841.Repositories.Impl;
using Group5_SWD392_SE1841.Repositories.Iplm;
using Group5_SWD392_SE1841.Services;
using Group5_SWD392_SE1841.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Group5Swd392Se1841Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register the repository
builder.Services.AddScoped<ITimesheetRepo, TimesheetRepo>();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();
builder.Services.AddScoped<IProjectRepo, ProjectRepo>();


// Register the service
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
var app = builder.Build();

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
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
