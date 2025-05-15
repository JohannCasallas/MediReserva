using MediReserva.Components;
using MediReserva.Data;
using MediReserva.Middleware;
using MediReserva.Services;
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

            // Inyección de dependencias para servicios de aplicación
            builder.Services.AddScoped<ICitaService, CitaService>();
            builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
            builder.Services.AddScoped<IMedicoService, MedicoService>();
            builder.Services.AddScoped<IPacienteService, PacienteService>();


            // Registro de controladores API
            builder.Services.AddControllers();

            // Documentación Swagger para APIs
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuración de componentes Razor interactivos (Blazor Server)
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Middleware de manejo global de errores
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Configuración del pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // Mapeo de componentes Blazor
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Mapeo de controladores API
            app.MapControllers();

            app.Run();
        }
    }
}
