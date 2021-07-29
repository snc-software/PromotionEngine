using MediatR;
using PromotionEngine.Domain;
using PromotionEngine.ServiceModel.Responses;

namespace PromotionEngine.ServiceModel.Requests
{
    public class CalculateBasketTotalCommand : IRequest<CalculateBasketTotalCommandResponse>
    {
        public Basket Basket { get; set; }
    }
}