using System;
using System.Collections.Generic;

namespace PromotionEngine.Domain
{
    public class BasketDetailModel
    {
        public BasketDetailModel()
        {
            PromotionsApplied = new List<string>();
        }
        
        public Guid BasketDetailId { get; set; } 
        public List<string> PromotionsApplied { get; set; } 
        
        public decimal Total { get; set; }
    }
}