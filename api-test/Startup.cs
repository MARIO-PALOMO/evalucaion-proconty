using api_test.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace api_test
{
    public class Startup
    {
        private readonly string migrationTableName = "__testMigrationsHistory";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("TodosLosOrigenes", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api_test", Version = "v1" });
            });

            ConfigDatabaseConnection(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_test v1"));
            }

            app.UseCors("TodosLosOrigenes");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigDatabaseConnection(IServiceCollection services)
        {
            var testConnectionString = Configuration.GetConnectionString("TestConnectionString");
            if (string.IsNullOrEmpty(testConnectionString))
            {
                throw new Exception("Cadena de conexion de BDD no configurada: testConnectionString");
            }

            services.AddDbContext<pruebaContext>(
                (serviceProvider, options) =>
                {
                    options.UseSqlServer(testConnectionString,
                        x =>
                        {
                            x.MigrationsHistoryTable(migrationTableName);
                            x.EnableRetryOnFailure(5);
                        }).EnableSensitiveDataLogging();
                });
        }
    }
}
