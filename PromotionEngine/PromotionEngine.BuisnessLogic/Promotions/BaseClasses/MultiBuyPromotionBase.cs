using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Promotions.BaseClasses
{
    public abstract class MultiBuyPromotionBase
    {
        protected abstract int PromotionalQuantity { get; }
        
        protected abstract decimal PromotionalPrice { get; }
        
        protected void ApplyMultiBuyPromotion(BasketLineModel basketLine)
        {
            var promotionalQtyConfig = GetPromotionalQuantitiesConfiguration(basketLine);
            CalculateLineTotal(basketLine, promotionalQtyConfig);
        }

        protected bool CanApplyPromotion(BasketLineModel basketLine)
        {
            return basketLine != null &&
                   basketLine.Quantity >= PromotionalQuantity;
        }

        #region Private Functionality
        
        PromotionalQuantitiesConfiguration GetPromotionalQuantitiesConfiguration(BasketLineModel basketLine)
        {
            return new(basketLine.Quantity / PromotionalQuantity,
                basketLine.Quantity % PromotionalQuantity);
        }
        
        void CalculateLineTotal(BasketLineModel basketLine, PromotionalQuantitiesConfiguration promotionalQtyConfig)
        {
            basketLine.LineTotal = promotionalQtyConfig.TimesToApplyPromotion * PromotionalPrice +
                                   promotionalQtyConfig.RemainingQuantity * basketLine.Product.Price;
        }
        
        
        #endregion
    }
}