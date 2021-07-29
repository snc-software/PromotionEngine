using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromotionEngine.ServiceInterface;
using PromotionEngine.ServiceModel.Requests;

namespace PromotionEngine.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateBasketTotalController : ControllerBase
    {
        readonly IMediator _mediator;

        public CalculateBasketTotalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Calculate([FromBody] CalculateBasketTotalRequest request)
        {
            try
            {
                var command = new CalculateBasketTotalCommand
                {
                    Basket = request.Basket
                };

                var response = await _mediator.Send(command);

                return Ok(new CalculateBasketTotalResponse(response.Basket));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}