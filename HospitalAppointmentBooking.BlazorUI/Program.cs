using HospitalAppointmentBooking.Application.Extensions;
using HospitalAppointmentBooking.BlazorUI.Components;
using HospitalAppointmentBooking.Infrastructure.Extensions;
using HospitalAppointmentBooking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructure(builder.Configuration).AddApplication();

var apiBase = builder.Configuration["ApiBaseUrl"];
builder.Services.AddHttpClient("HospitalApi", client =>
{
    client.BaseAddress = new Uri(apiBase!);
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HospitalApi"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add authentication and authorization services
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.UseRouting();             // Add routing

app.UseAuthentication();      // Add authentication middleware
app.UseAuthorization();       // Add authorization middleware

app.UseAntiforgery();         // Add antiforgery middleware AFTER authorization but BEFORE endpoint mapping

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
