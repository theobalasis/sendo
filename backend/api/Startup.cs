using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sendo.Api.Config;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;
using Sendo.Api.Endpoints.Security;

namespace Sendo.Api
{
    /// <summary>
    /// Configures the environment and services of the web application.
    /// </summary>
    public class Startup
    {
        private IWebHostEnvironment? Environment { get; set; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ISettings settings;
            if (Environment == null || Environment.IsDevelopment())
            {
                services.AddSingleton<ISettings, DevSettings>();
                settings = new DevSettings();
            }
            else
            {
                services.AddSingleton<ISettings, ProductionSettings>();
                settings = new ProductionSettings();
            }

            services.AddDbContext<UserDataPostgresContext>(
                opt => opt.UseNpgsql(
                    settings.DataDbConnectionString,
                    action => action.MigrationsHistoryTable("migrations_history", "management")
                )
            );

            services.AddHttpContextAccessor();

            // Repositories are added as transient so as to prevent the persistence of
            // transactions across unrelated operations.
            services.AddTransient<IRepository<Campaign>, DbRepository<Campaign, UserDataPostgresContext>>();
            services.AddTransient<IRepository<Contact>, DbRepository<Contact, UserDataPostgresContext>>();
            services.AddTransient<IRepository<ContactGroup>, DbRepository<ContactGroup, UserDataPostgresContext>>();
            services.AddTransient<IRepository<MailTemplate>, DbRepository<MailTemplate, UserDataPostgresContext>>();

            services.AddTransient<IDbRepository<Campaign>, DbRepository<Campaign, UserDataPostgresContext>>();
            services.AddTransient<IDbRepository<Contact>, DbRepository<Contact, UserDataPostgresContext>>();
            services.AddTransient<IDbRepository<ContactGroup>, DbRepository<ContactGroup, UserDataPostgresContext>>();
            services.AddTransient<IDbRepository<MailTemplate>, DbRepository<MailTemplate, UserDataPostgresContext>>();

            services.AddScoped<IAuthenticationService, SessionAuthenticationService>();
            services.AddScoped<IEntityAuthorizationService<Campaign>, CampaignAuthorizationService>();
            services.AddScoped<IEntityAuthorizationService<Contact>, ContactAuthorizationService>();
            services.AddScoped<IEntityAuthorizationService<ContactGroup>, ContactGroupAuthorizationService>();
            services.AddScoped<IEntityAuthorizationService<MailTemplate>, MailTemplateAuthorizationService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sendo API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Environment = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sendo API v1"));
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
