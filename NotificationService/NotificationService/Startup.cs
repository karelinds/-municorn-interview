using System.Linq;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NotificationLib.Message;
using NotificationService.ControllerFactory;

namespace NotificationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNotificationsSender();
            services.AddRouting();
            services.AddControllers()
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<NotificationMessage>(lifetime: ServiceLifetime.Singleton));

            services.AddMvc(o =>
                {
                    o.Conventions.Add(new GenericNotificationNameConvention());
                })
                .ConfigureApplicationPartManager(m =>
                {
                    m.FeatureProviders.Add(new GenericNotificationControllerFeatureProvider());
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationService", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotificationService v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}