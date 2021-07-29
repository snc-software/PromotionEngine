using PromotionEngine.Domain;

namespace PromotionEngine.ServiceInterface
{
    public record CalculateBasketTotalRequest(Basket Basket);

    public record CalculateBasketTotalResponse(Basket Basket);
}