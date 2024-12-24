using Dev.LeaveApplication.Data.Database;
using Dev.LeaveApplication.Data.Managers;
using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Web.Managers;
using Dev.LeaveApplication.Web.Managers.Interfaces;
using Dev.LeaveApplication.Web.Services;
using Dev.LeaveApplication.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("LeaveAppAuth")
    .AddCookie("LeaveAppAuth", options =>
	{
		options.LoginPath = "/User/Login";
		options.Cookie.Name = "LeaveAppAuth";
	});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<LeaveApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Data - Managers
builder.Services.AddScoped<IFormManager, FormManager>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IUserManager, UserManager>();

//Web - Services
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=SignIn}/{id?}");

app.Run();
