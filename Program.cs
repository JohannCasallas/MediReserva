using MediReserva.Components;
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

            // Configura el contexto de base de datos con la cadena de conexi�n
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaDB")));

            // Exponer servicios aqu�
            builder.Services.AddScoped<IPacienteService, PacienteService>();
            builder.Services.AddScoped<IMedicoService, MedicoService>();
            builder.Services.AddScoped<ICitaService, CitaService>();
            builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();

            // Agrega servicios de Razor
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configura el pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
