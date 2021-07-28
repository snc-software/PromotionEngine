using System.Linq;
using PromotionEngine.BuisnessLogic.Promotions.BaseClasses;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Promotions
{
    public class SkuBMultiBuyPromotion : MultiBuyPromotionBase, IPromotion
    {
        protected override int PromotionalQuantity => 2;
        protected override decimal PromotionalPrice => 45M;
        public string Description => "2 of B's for 45";
        public Basket ApplyPromotionIfApplicable(Basket basket)
        {
            var basketLine = basket.BasketLines.FirstOrDefault(x => x.Product.Sku == 'B');
            if (CanApplyPromotion(basketLine))
            {
                ApplyPromotion(basket, basketLine);
            }
            return basket;
        }
        
        
        #region Private Functionality
        
        void ApplyPromotion(Basket basket, BasketLineModel basketLine)
        {
            ApplyMultiBuyPromotion(basketLine);
            basket.BasketDetail.PromotionsApplied.Add(Description);
        }
        
        #endregion
    }
}