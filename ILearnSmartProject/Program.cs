using ILearnSmartProject.Models;
using ILearnSmartProject.Payment.StripeManager;
using ILearnSmartProject.Repositories;
using ILearnSmartProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserAppService>();

builder.Services.AddScoped<ICheckOutSession,StripeAdaptorManager>();
// ADDING CheckoutAppSercice to buildier class for me to usse it in thehome controller
builder.Services.AddScoped<CheckOutAppService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddDbContext<LearnSmartContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<StripeModel>(builder.Configuration.GetSection("Stripe"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
