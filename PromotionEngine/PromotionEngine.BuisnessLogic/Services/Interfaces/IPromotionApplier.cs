using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Services.Interfaces
{
    public interface IPromotionApplier
    {
        Basket ApplyPromotions(Basket basket);
    }
}