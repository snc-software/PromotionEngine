using System;

namespace PromotionEngine.Domain
{
    public class BasketLineModel
    {
        public Guid BasketLineId { get; set; } 
        
        public Guid BasketDetailId { get; set; } 
        
        public ProductModel Product { get; set; } 
        
        public int Quantity { get; set; }
        
        public decimal LineTotal { get; set; }
        
        public bool PromotionApplied { get; set; }
    }
}