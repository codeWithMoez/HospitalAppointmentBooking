using HospitalAppointmentBooking.Application.Extensions;
using HospitalAppointmentBooking.BlazorUI.Components;
using HospitalAppointmentBooking.Infrastructure.Extensions;

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
