using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Repositories;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Teknokent.Models;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddLocalization(options => 
{
        options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    { 
       new CultureInfo("en-US"),
       new CultureInfo("tr-TR")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedUICultures = supportedCultures;
});


builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IBoardOfMemberRepository, BoardOfMemberRepository>();
builder.Services.AddScoped<ICareerPostingRepository, CareerPostingRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICvRepository, CvRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();

builder.Services.AddScoped<IRefereeRegistrationRepository, RefereeRegistrationRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ICampusPreliminaryAppFormRepository, CampusPreliminaryAppFormRepository>();
builder.Services.AddScoped<ILegislationRepository, LegislationRepository>();
builder.Services.AddScoped<ITtoRepository, TtoRepository>();
builder.Services.AddScoped<IGeneralInformationRepository, GeneralInformationRepository>();
builder.Services.AddScoped<IManagementOfficeRepository, ManagementOfficeRepository>();

builder.Services.AddTransient<FileUploadService>();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>() ;


builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(6);
    opt.Lockout.MaxFailedAccessAttempts = 4;
    opt.SignIn.RequireConfirmedEmail = false;

});



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserAndAdmin", policy => policy.RequireRole("Admin").RequireRole("User"));
});



var app = builder.Build();


app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
