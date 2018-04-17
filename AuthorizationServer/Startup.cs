using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityServer4;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AuthorizationServer.Models;
using IdentityServer4.Stores;
using AuthorizationServer.Stores;
using Swashbuckle.AspNetCore.Swagger;
using AuthorizationServer.Handlers;
using AuthorizationServer.Data;

namespace AuthorizationServer
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment;
        private IConfigurationSection _appSettings;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //Configuration = configuration;
            _hostingEnvironment = env;

            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddJsonFile("appsettings-custom.json", optional: true)
             .AddJsonFile($"appsettings-custom.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();
            Configuration = builder.Build();

            _appSettings = Configuration.GetSection("AppSettings");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddAuthentication(o => o.AddScheme("api", a => a.HandlerType = typeof(TokenHandler)));

            ///*
            //  * Add to .csproj
            //  * 
            //   <ItemGroup>
            //     <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0-preview1-final" />
            //     <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0-preview1-final" />
            //   </ItemGroup>
            //*/
            services.AddDbContext<UserDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultDbContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDBContext>();
               // .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddSingleton<IClientStore, CustomClientStore>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddSigningCredential(new X509Certificate2(Path.Combine("..", "..", "certs", "IdentityServer4Auth.pfx")))
                .AddInMemoryApiResources(ApiResourceProvider.GetAllResources())
                .AddAspNetIdentity<ApplicationUser>();

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AuthServer", Version = "v1" });
            });

            services.Configure<AppSettings>(_appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            RoleManager<IdentityRole> roleManager, UserDBContext context, IDbInitializer dbInitializer)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseIdentity(); Is obsolete, use instead app.UseAuthentication();
            app.UseAuthentication();

            // Note that UseIdentityServer must come after UseIdentity in the pipeline
            app.UseIdentityServer();

            //Generate EF Core Seed Data
            dbInitializer.Initialize();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthServer V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
