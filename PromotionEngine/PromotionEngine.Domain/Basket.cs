using System.Collections.Generic;

namespace PromotionEngine.Domain
{
    public class Basket
    {
        public BasketDetailModel BasketDetail { get; set; } 
        
        public List<BasketLineModel> BasketLines { get; set; }   
    }
}