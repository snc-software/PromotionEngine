using System.Linq;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Promotions
{
    public class SkuCAndDFixedPricePromotion : IPromotion
    {
        decimal _promotionalPrice => 30;
       
        public string Description => "C & D For 30";
        
        public Basket ApplyPromotionIfApplicable(Basket basket)
        {
            var basketLineForSkuC = basket.BasketLines.FirstOrDefault(x => x.Product.Sku == 'C');
            var basketLineForSkuD = basket.BasketLines.FirstOrDefault(x => x.Product.Sku == 'D');
            
            if (CanApplyPromotion(basketLineForSkuC, basketLineForSkuD))
            {
                ApplyPromotion(basket, basketLineForSkuC, basketLineForSkuD);
            }

            return basket;
        }

        #region Private Functionality
        
        bool CanApplyPromotion(BasketLineModel skuCBasketLine, BasketLineModel skuDBasketLine)
        {
            return skuCBasketLine?.Quantity > 0 && skuDBasketLine?.Quantity > 0;
        }
        
        void ApplyPromotion(Basket basket, BasketLineModel? basketLineForSkuC, BasketLineModel? basketLineForSkuD)
        {
            if (MoreOfSkuCIsOrdered(basketLineForSkuC, basketLineForSkuD))
            {
                ApplyPromotionWhenMoreOfSkuCIsOrdered(basketLineForSkuC, basketLineForSkuD);
            }
            else
            {
                ApplyPromotionWhenMoreOfSkuDIsOrdered(basketLineForSkuC, basketLineForSkuD);
            }

            basket.BasketDetail.PromotionsApplied.Add(Description);
        }
        
        static bool MoreOfSkuCIsOrdered(BasketLineModel? basketLineForSkuC, BasketLineModel? basketLineForSkuD)
        {
            return basketLineForSkuC.Quantity >= basketLineForSkuD.Quantity;
        }
        
        void ApplyPromotionWhenMoreOfSkuCIsOrdered(BasketLineModel? basketLineForSkuC, BasketLineModel? basketLineForSkuD)
        {
            basketLineForSkuC.LineTotal =
                (basketLineForSkuC.Quantity - basketLineForSkuD.Quantity) * basketLineForSkuC.Product.Price;
            basketLineForSkuD.LineTotal = basketLineForSkuD.Quantity * _promotionalPrice;
        }
        
        void ApplyPromotionWhenMoreOfSkuDIsOrdered(BasketLineModel? basketLineForSkuC, BasketLineModel? basketLineForSkuD)
        {
            basketLineForSkuC.LineTotal = basketLineForSkuC.Quantity * _promotionalPrice;
            basketLineForSkuD.LineTotal =
                (basketLineForSkuD.Quantity - basketLineForSkuC.Quantity) * basketLineForSkuD.Product.Price;
        }
        
        #endregion
    }
}