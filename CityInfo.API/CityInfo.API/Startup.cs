using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API
{
    public class Startup
    {


        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // Called by the host before the Configure method to configure the app's services.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            ///Set the DbContext
            ///the ASP.NET Core configuration system reads the connection string from the appsettings.json file.
            services.AddDbContext<CityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );



            services.AddMvc().
                AddMvcOptions(o=>o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));


#if DEBUG
            services.AddTransient<IMailService, LocalMailService>(); //this allow to inject this service on controllers
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityDbContext cityDbContext)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                cityDbContext.EnsureSeedDataCreatedForContextDB();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //add mvc to the request pipeline , after having added the exception handler.
            app.UseMvc();


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
