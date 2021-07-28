using System.Collections.Generic;

namespace PromotionEngine.Domain
{
    public class Basket
    {
        public Basket()
        {
            BasketDetail = new BasketDetailModel();
            BasketLines = new List<BasketLineModel>();
        }
        
        public BasketDetailModel BasketDetail { get; set; } 
        
        public List<BasketLineModel> BasketLines { get; set; }   
    }
}