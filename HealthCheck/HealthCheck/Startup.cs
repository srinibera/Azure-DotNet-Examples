using HealthCheck.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck
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
            string accountName = "test";
            string accountKey = "3333333-22222"; //from keyvault
            string blobConString = $"DefaultEndpointsProtocol = https; AccountName ={ accountName}; AccountKey ={ accountKey}; EndpointSuffix = core.windows.net";
            //Register helath checks
            services.AddHealthChecks()
                .AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"]) 
                //.AddCheck<SqlConnectionHealthCheck>("SQL") //Customized
                .AddCheck<DownstreamConnectionsCheck>("DownStream")
                .AddCheck<UpstreamConnectionsCheck>("upstream")
                .AddAzureBlobStorage(blobConString);

            services.AddControllers();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();              
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Add endpoint for heath
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
