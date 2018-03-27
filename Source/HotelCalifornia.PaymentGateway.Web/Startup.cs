using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelCalifornia.PaymentGateway.Domain;
using HotelCalifornia.PaymentGateway.Domain.IntegrationAggregate;
using HotelCalifornia.PaymentGateway.Domain.PaymentSessionAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelCalifornia.PaymentGateway.Web
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _hostingEnvironment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddCors();
            services.AddMvc().AddFeatureFolders();
            services.AddScoped<IIntegrationRepository, IntegrationRepository>();
            services.AddScoped<IPaymentSessionRepository, PaymentSessionRepository>();

            var physicalProvider = this._hostingEnvironment.ContentRootFileProvider;
            services.AddSingleton<IFileProvider>(physicalProvider);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (this._hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseStaticFiles();

            app.UseMvc();

        }
    }
}
