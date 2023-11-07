using Microsoft.EntityFrameworkCore;
using TrabajoFinalProgramacion.Models;


using TrabajoFinalProgramacion.Servicios.Contrato;
using TrabajoFinalProgramacion.Servicios.Implementacion;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

builder.Services.AddDbContext<DblogContext>(opciones => opciones.UseMySql(connectionString, serverVersion));



builder.Services.AddScoped<IUsuarioService, UsuarioService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

//Sirve para que cuando te deslogees y quieras hacer para atrás no te lo permita.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true,
            Location = ResponseCacheLocation.None,

        }
       );
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
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();


