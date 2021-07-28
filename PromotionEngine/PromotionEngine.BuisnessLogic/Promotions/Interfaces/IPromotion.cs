using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Promotions.Interfaces
{
    public interface IPromotion
    {
        string Description { get; }
        
        Basket ApplyPromotionIfApplicable(Basket basket);
    }
}