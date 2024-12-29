using Microsoft.EntityFrameworkCore;
using ProjetoExemplo.Data;
using ProjetoExemplo.Repositories;
using ProjetoExemplo.Repositories.Implementations;
using ProjetoExemplo.Services;

namespace ProjetoExemplo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // postgreSQL configuration
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configures the repository and IBGE service.
            builder.Services.AddScoped<IProcessoRepository, ProcessoRepository>();
            builder.Services.AddHttpClient<IbgeApiService>();
            builder.Services.AddScoped<ProcessoService>();

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
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Processo}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
