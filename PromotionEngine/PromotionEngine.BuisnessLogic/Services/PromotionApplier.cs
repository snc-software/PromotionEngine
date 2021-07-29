using System.Collections.Generic;
using System.Linq;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.BuisnessLogic.Services.Interfaces;
using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Services
{
    public class PromotionApplier : IPromotionApplier
    {
        readonly IEnumerable<IPromotion> _promotions;

        public PromotionApplier(IEnumerable<IPromotion> promotions)
        {
            _promotions = promotions;
        }
        
        public Basket ApplyPromotions(Basket basket)
        {
            return _promotions.Aggregate(basket, (current, promotion) => promotion.ApplyPromotionIfApplicable(current));
        }
    }
}