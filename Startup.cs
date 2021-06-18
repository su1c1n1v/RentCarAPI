using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using RentCarAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI
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
            services.AddDbContext<ApplicationContext>(Temp => Temp.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddNewtonsoftJson(Temp => {
                Temp.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddScoped<IUsersRepo, SqlUsersRepo>();
            services.AddScoped<IRegistersRepo, SqlRegistesRepo>();
            services.AddScoped<ICarsRepo, SqlCarsRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
