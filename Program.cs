using AdmissionData.Dapper;
using AdmissionData;
using AdmissionModel;
using AdmissionRepo.Utilities;
using AdmissionRepo;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;


using System.Text.Json.Serialization;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


ConfigurationHelper.Initialize(builder.Configuration);
builder.Services.AddTransient<IDapperContext>(x => new DapperContext(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IMailClient, MailClient>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();


builder.Services.AddCors(options =>
{
    options.AddPolicy("myCors", builder =>
    {
        builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddRazorPages();


//for anti fogery token
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"wwwroot\keys"))
    .SetApplicationName("adm")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(30)); //default is 90 days

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AdmApp";
        options.LoginPath = new PathString("/account/login");
        options.SlidingExpiration = true;
        options.AccessDeniedPath = new PathString("/account/unauthorize");

        //https://bit.ly/2JU6CNH
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });
builder.Services.AddSession();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

#if DEBUG
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //redirect to www
    //var options = new RewriteOptions();
    //options.AddRedirectToWww();
    //app.UseRewriter(options);

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();
app.UseCookiePolicy();
app.UseRouting();



//For IP Address
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseCors("myCors");
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Important: Install Microsoft Visual C++ Redistributable 2017 (x86): https://bit.ly/2KIu4hi
RotativaConfiguration.Setup(builder.Environment.WebRootPath, "Rotativa");

app.Run();
