using Dev.LeaveApplication.Data.Database;
using Dev.LeaveApplication.Data.Managers;
using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Web.Managers;
using Dev.LeaveApplication.Web.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<LeaveApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFormManager, FormManager>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();

builder.Services.AddScoped<IFormService, FormService>();


//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
