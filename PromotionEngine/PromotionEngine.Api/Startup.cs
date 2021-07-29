using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PromotionEngine.BuisnessLogic.Promotions;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.BuisnessLogic.Services;
using PromotionEngine.BuisnessLogic.Services.Interfaces;
using PromotionEngine.ServiceModel.Requests;

namespace PromotionEngine.Api
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
            services.AddScoped<IPromotion, SkuAMultiBuyPromotion>();
            services.AddScoped<IPromotion, SkuBMultiBuyPromotion>();
            services.AddScoped<IPromotion, SkuCAndDFixedPricePromotion>();
            services.AddScoped<IPromotionApplier, PromotionApplier>();
            services.AddMediatR(typeof(CalculateBasketTotalCommand));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "PromotionEngine.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PromotionEngine.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}