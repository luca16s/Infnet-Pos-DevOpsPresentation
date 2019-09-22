using AutoMapper;
using DeadFishStudio.Product.Infrastructure.CrossCutting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeadFishStudio.Product.Application.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appSettings.json")
                .Build();
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //IConfiguration config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false, true)
            //    //.AddJsonFile($"appsettings.{environment}.json", true)
            //    //.AddEnvironmentVariables()
            //    .Build();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ProductConfiguration>(Configuration.GetSection(nameof(ProductConfiguration)));
            Injection.Services(services, Configuration);
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}