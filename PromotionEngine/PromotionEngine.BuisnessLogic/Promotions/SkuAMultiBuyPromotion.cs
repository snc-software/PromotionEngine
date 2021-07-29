using System.Linq;
using PromotionEngine.BuisnessLogic.Promotions.BaseClasses;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Promotions
{
    public class SkuAMultiBuyPromotion : MultiBuyPromotionBase, IPromotion
    {
        public string Description => "3 of A's for 130";

        protected override int PromotionalQuantity => 3;

        protected override decimal PromotionalPrice => 130;
        
        public Basket ApplyPromotionIfApplicable(Basket basket)
        {
            var basketLine = basket.BasketLines.FirstOrDefault(x => x.Product.Sku == 'A');
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
            basketLine.PromotionApplied = true;
            basket.BasketDetail.PromotionsApplied.Add(Description);
        }
        
        #endregion
    }
}