using ILearnSmartProject.Models;
using ILearnSmartProject.Payment.StripeManager;
using ILearnSmartProject.Repositories;
using ILearnSmartProject.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserAppService>();
builder.Services.AddScoped<CoursesUserPurchaseService>();
builder.Services.AddScoped<CourseAppService>();
builder.Services.AddScoped<EmailAppService>();



builder.Services.AddScoped<ICheckOutSession,StripeAdaptorManager>();
// ADDING CheckoutAppSercice to buildier class for me to usse it in thehome controller
builder.Services.AddScoped<CheckOutAppService>();


builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CoursesUserPurchaseRepository>();



builder.Services.AddDistributedMemoryCache();
// setting up the session usage for this application
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddDbContext<LearnSmartContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<StripeModel>(builder.Configuration.GetSection("Stripe"));
builder.Services.Configure<Course>(builder.Configuration.GetSection("AzureBlobStorage"));
builder.Services.Configure<AzureBlobModel>(builder.Configuration.GetSection("AzureBlobStorage"));
builder.Services.Configure<SMTPConnection>(builder.Configuration.GetSection("SMTP"));




builder.Services.Configure<KestrelServerOptions>(options => {
    options.Limits.MaxRequestBodySize = null; // Unlimited
});

// solving The page was not displayed because the request entity is too large. error in uploading to blob storage by setting the max request body size to unlimited in kestrel server options.
builder.Services.Configure<IISServerOptions>(options => {
    options.MaxRequestBodySize = null; // Unlimited
});


builder.Services.Configure<KestrelServerOptions>(options => {
    options.Limits.MaxRequestBodySize = null; // 100 MB
});

builder.Services.Configure<FormOptions>(options => {
    options.MultipartBodyLengthLimit = long.MaxValue;
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = null; // Unlimited
});
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
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
