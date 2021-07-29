using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.BuisnessLogic.Promotions;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.BuisnessLogic.Services;
using PromotionEngine.BuisnessLogic.Services.Interfaces;
using PromotionEngine.ServiceModel.Requests;

namespace PromotionEngine.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        public ServiceProvider ServiceProvider { get; set; }

        public virtual void Setup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IPromotion, SkuAMultiBuyPromotion>();
            services.AddScoped<IPromotion, SkuBMultiBuyPromotion>();
            services.AddScoped<IPromotion, SkuCAndDFixedPricePromotion>();
            services.AddScoped<IPromotionApplier, PromotionApplier>();
            services.AddMediatR(typeof(CalculateBasketTotalCommand));
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}