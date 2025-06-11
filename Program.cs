using Michael_Jose_AP1_P1.Components;
using Michael_Jose_AP1_P1.DAL;
using Michael_Jose_AP1_P1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Obtenemos el ConStr para usarlo en el contexto
var ConStr = builder.Configuration.GetConnectionString("Postgres");

// Agremos el contexto al builder con el ConStr
builder.Services.AddDbContextFactory<Contexto>(o => o.UseNpgsql(ConStr));

// Inyeccion del service
builder.Services.AddScoped<AportesServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

