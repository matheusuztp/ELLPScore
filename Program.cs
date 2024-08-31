using ELLPScore.Context.DB;
using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ELLPScoreDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Professor>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ELLPScoreDBContext>();

builder.Services.AddRazorPages();

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
        "/Identity/Account/ForgotPassword"
    };

    if (!context.User.Identity.IsAuthenticated &&
        !allowedPaths.Any(path => context.Request.Path.StartsWithSegments(path)))
    {
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }

    await next();
});

app.MapRazorPages();
app.Run();
