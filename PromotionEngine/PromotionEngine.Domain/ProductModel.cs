using System;

namespace PromotionEngine.Domain
{
    public record ProductModel(Guid Id, char Sku, int Price);
}