using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.Persistance;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.Customers.Queries.GetCustomerList;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using Northwind.Application.Customers.Commands.DeleteCustomer;
using Swashbuckle.AspNetCore.Swagger;

namespace Northwind.Presentation
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<NorthwindContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Northwind")));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Northwind API", Version = "v1" });
            });

            // Add application services.
            services.AddScoped<IGetCustomerDetailQuery, GetCustomerDetailQuery>();
            services.AddScoped<IGetCustomerListQuery, GetCustomerListQuery>();
            services.AddScoped<ICreateCustomerCommand, CreateCustomerCommand>();
            services.AddScoped<IUpdateCustomerCommand, UpdateCustomerCommand>();
            services.AddScoped<IDeleteCustomerCommand, DeleteCustomerCommand>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
                NorthwindContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapWhen(a => !a.Request.Path.Value.StartsWith("/swagger"), builder =>
                builder.UseMvc(routes =>
                {
                    routes.MapSpaFallbackRoute(
                        name: "spa-fallback",
                        defaults: new { controller = "Home", action = "Index" });
                })
            );

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind API V1");
            });

            NorthwindInitializer.Initialize(context);
        }
    }
}
