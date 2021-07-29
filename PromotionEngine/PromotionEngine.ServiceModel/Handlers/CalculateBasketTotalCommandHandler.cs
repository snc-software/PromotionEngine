using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.BuisnessLogic.Services.Interfaces;
using PromotionEngine.Domain;
using PromotionEngine.ServiceModel.Requests;
using PromotionEngine.ServiceModel.Responses;

namespace PromotionEngine.ServiceModel.Handlers
{
    public class CalculateBasketTotalCommandHandler : IRequestHandler<CalculateBasketTotalCommand, CalculateBasketTotalCommandResponse>
    {
        readonly IPromotionApplier _promotionApplier;

        public CalculateBasketTotalCommandHandler(IPromotionApplier promotionApplier)
        {
            _promotionApplier = promotionApplier;
        }
        
        public async Task<CalculateBasketTotalCommandResponse> Handle(CalculateBasketTotalCommand request, CancellationToken cancellationToken)
        {
            var response = new CalculateBasketTotalCommandResponse
            {
                Basket = request.Basket
            };
            
            var updatedBasket = _promotionApplier.ApplyPromotions(response.Basket);
            updatedBasket.BasketDetail.Total = updatedBasket.BasketLines.Sum(GetBasketLineTotal);
            return response;
        }
        
        #region Private Functionality
        
        static decimal GetBasketLineTotal(BasketLineModel basketLine)
        {
            if (basketLine.PromotionApplied)
            {
                return basketLine.LineTotal;
            }

            return basketLine.Quantity * basketLine.Product.Price;
        }
        
        #endregion
    }
}