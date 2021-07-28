using System;
using System.Linq;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Promotions
{
    public class SkuAMultiBuyPromotion : IPromotion
    {
        public string Description => "3 of A's for 130";

        public int PromotionalQuantity => 3;

        public decimal PromotionalPrice => 130;
        
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
        
        bool CanApplyPromotion(BasketLineModel basketLine)
        {
            return basketLine != null &&
                   basketLine.Quantity >= PromotionalQuantity;
        }

        void ApplyPromotion(Basket basket, BasketLineModel basketLine)
        {
            ApplyMultiBuyPromotion(basketLine);
            basket.BasketDetail.PromotionsApplied.Add(Description);
        }
        
        protected void ApplyMultiBuyPromotion(BasketLineModel basketLine)
        {
            var timesToApplyOffer = basketLine.Quantity / PromotionalQuantity;
            var numberOfItemsNotApplicableForPromotion = basketLine.Quantity % PromotionalQuantity;

            basketLine.LineTotal = timesToApplyOffer * PromotionalPrice + 
                                   numberOfItemsNotApplicableForPromotion * basketLine.Product.Price;
        }
        
        #endregion
    }
}