using ELLPScore.Context.DB;
using ELLPScore.Domain;
using ELLPScore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("pt-BR") // Adiciona o suporte para português (Brasil)
    };

    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ELLPScoreDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.Events.OnRedirectToLogin = context =>
    {
        // Se não houver ReturnUrl, redireciona para o Dashboard
        if (string.IsNullOrEmpty(context.Request.Path) ||
            context.Request.Path == options.LoginPath)
        {
            context.Response.Redirect("/Index");
        }
        else
        {
            context.Response.Redirect($"{options.LoginPath}?{options.ReturnUrlParameter}={context.Request.Path}");
        }
        return Task.CompletedTask;
    };
});

builder.Services.AddDefaultIdentity<Professor>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<ELLPScoreDBContext>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var allowedPaths = new[]
    {
        "/Identity/Account/Login",
        "/Identity/Account/Register",
        "/Identity/Account/ForgotPassword",
        "/Identity/Account/ResetPassword", 
        "/Identity/Account/ResetPasswordConfirmation",
        "/Identity/Account/ForgotPasswordConfirmation",
        "/Identity/Account/ConfirmEmail", 
        "/Identity/Account/ConfirmEmailChange" 
    };

    if (!context.User.Identity.IsAuthenticated &&
        !allowedPaths.Any(path => context.Request.Path.StartsWithSegments(path)))
    {
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }

    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
