using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using WebCalc.Interfaces;
using WebCalc.Models;
using WebCalc.Services;

namespace WebCalc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();



            services.AddScoped<PlusOperator>();
            services.AddScoped<MinusOperator>();
            services.AddScoped<DivideOperator>();
            services.AddScoped<MultiOperator>();

            services.AddByName<IOperator>()

                .Add<PlusOperator>("+")
               .Add<MinusOperator>("-")
                .Add<DivideOperator>("/")
               .Add<MultiOperator>("*")

               .Build();


            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddScoped<ICalculateService, CalculateService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
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
        }
    }
}
