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

        public static Basket Scenario1 => new()
        {
            BasketLines = new List<BasketLineModel>
            {
                new()
                {
                    Product = ProductsIndexedBySku['A'],
                    Quantity = 1
                },
                new()
                {
                    Product = ProductsIndexedBySku['B'],
                    Quantity = 1
                },
                new()
                {
                    Product = ProductsIndexedBySku['C'],
                    Quantity = 1
                }
            }
        };
        
        public static Basket Scenario2 => new()
        {
            BasketLines = new List<BasketLineModel>
            {
                new()
                {
                    Product = ProductsIndexedBySku['A'],
                    Quantity = 5
                },
                new()
                {
                    Product = ProductsIndexedBySku['B'],
                    Quantity = 5
                },
                new()
                {
                    Product = ProductsIndexedBySku['C'],
                    Quantity = 1
                }
            }
        };
        
        public static Basket Scenario3 => new()
        {
            BasketLines = new List<BasketLineModel>
            {
                new()
                {
                    Product = ProductsIndexedBySku['A'],
                    Quantity = 3
                },
                new()
                {
                    Product = ProductsIndexedBySku['B'],
                    Quantity = 5
                },
                new()
                {
                    Product = ProductsIndexedBySku['C'],
                    Quantity = 1
                },
                new()
                {
                    Product = ProductsIndexedBySku['D'],
                    Quantity = 1
                }
            }
        };
    }
}