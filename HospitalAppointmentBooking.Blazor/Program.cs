using HospitalAppointmentBooking.Application.Extensions;
using HospitalAppointmentBooking.Blazor.Components;
using HospitalAppointmentBooking.Blazor.Components.Account;
using HospitalAppointmentBooking.Blazor.Data;
using HospitalAppointmentBooking.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructure(builder.Configuration).AddApplication();

var apiBase = builder.Configuration["ApiBaseUrl"];
builder.Services.AddHttpClient("HospitalApi", client =>
{
    client.BaseAddress = new Uri(apiBase!);
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HospitalApi"));


// Authentication State
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();


// DbContext for Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity Setup
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

// Google External Login
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
})
.AddIdentityCookies();

// Email sender
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Razor Pages
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Identity endpoints
app.MapAdditionalIdentityEndpoints();

app.Run();
