using Cristian_Tavarez_P1_API1.Components;
using Microsoft.EntityFrameworkCore;
using Cristian_Tavarez_P1_API1.Models;
using Cristian_Tavarez_P1_API1.DAL;
using Cristian_Tavarez_P1_API1.Services; 

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar DbContext
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString)
);

// Registrar servicios de Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<AporteService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Construir la aplicación
var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
