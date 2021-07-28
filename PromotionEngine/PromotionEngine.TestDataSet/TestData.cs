using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Domain;

namespace PromotionEngine.TestDataSet
{
    public static class TestData
    {
        public static IEnumerable<ProductModel> Products =>
            new List<ProductModel>
            {
                new(Guid.NewGuid(), 'A', 50),
                new(Guid.NewGuid(), 'B', 30),
                new(Guid.NewGuid(), 'C', 20),
                new(Guid.NewGuid(), 'D', 10),
            };

        public static IDictionary<char, ProductModel> ProductsIndexedBySku =>
            Products.ToDictionary(k => k.Sku, v => v);
    }
}