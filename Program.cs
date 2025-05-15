using MediReserva.Components;
using MediReserva.Middleware;
using MediReserva.Models;
using MediReserva.Services.Implementations;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediReserva
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura el contexto de base de datos
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaDB")));

            // Inyecta los servicios de negocio
            builder.Services.AddScoped<IPacienteService, PacienteService>();
            builder.Services.AddScoped<IMedicoService, MedicoService>();
            builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();

            // Registra los controladores API
            builder.Services.AddControllers();

            // Registra Swagger para documentación de la API
            builder.Services.AddEndpointsApiExplorer(); // Necesario para exponer los endpoints
            builder.Services.AddSwaggerGen();           // Genera Swagger

            // Registra los servicios de Razor Components
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();// Manejo de erores

            // Configura el pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            else
            {
                // Habilita Swagger en entorno de desarrollo
                app.UseSwagger();      // Genera el archivo Swagger JSON
                app.UseSwaggerUI();    // Interfaz web en /swagger
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // Mapea componentes Blazor
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Mapea los controladores API (indispensable para Swagger)
            app.MapControllers();

            app.Run();
        }
    }
}
