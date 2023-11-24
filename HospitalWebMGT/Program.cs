using Hospital.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Hospital.Utilities;
using Hospital.Repositories.Implementation;
using Hospital.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hospital.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var Providers = builder.Services.BuildServiceProvider();
var Config = Providers.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(Config.GetConnectionString("dbcs")));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddTransient<IUnitofWork,UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddTransient<IHospitalInfo, HospitalInfoService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();

builder.Services.AddRazorPages();

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
DataSedding();
app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{Area=admin}/{controller=Hospitals}/{action=Index}/{id?}");

app.Run();
void DataSedding()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
