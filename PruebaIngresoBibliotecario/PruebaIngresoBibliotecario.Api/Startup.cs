using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaIngresoBibliotecario.Application.Commands;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces;
using PruebaIngresoBibliotecario.Domain.Services;
using PruebaIngresoBibliotecario.Infrastructure.Finders;
using PruebaIngresoBibliotecario.Infrastructure.Repositories;
using System;
using System.Diagnostics;

namespace PruebaIngresoBibliotecario.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.Indent();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new GuidConverter());
            });

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMediatR(typeof(PrestamoCommand).Assembly);

            services.AddSwaggerDocument();

            services.AddDbContext<Infrastructure.PersistenceContext>(opt =>
            {
                opt.UseInMemoryDatabase("PruebaIngreso");
            });

            services.AddControllers(mvcOpts =>
            {
            });
            services.AddScoped<IPrestamoService<Prestamo>, PrestamoService>();
            services.AddScoped<IPrestamoFinder<Prestamo>, PrestamoFinder>();
            services.AddScoped<IPrestamoRepository<Prestamo>, PrestamoRepository>();
           
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
            
        }
    }
}
