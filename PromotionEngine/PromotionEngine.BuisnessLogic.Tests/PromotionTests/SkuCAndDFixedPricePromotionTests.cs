using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using PromotionEngine.BuisnessLogic.Promotions;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.Domain;
using PromotionEngine.TestDataSet;

namespace PromotionEngine.BuisnessLogic.Tests.PromotionTests
{
    [TestFixture]
    public class SkuCAndDFixedPricePromotionTests
    {
        readonly IPromotion _promotion = new SkuCAndDFixedPricePromotion();
        IDictionary<char, ProductModel> _productsIndexedBySku;
        Basket _basket;
        
        
        [Test]
        public void IfTheBasketIsNotValidForThePromotionNoPromotionIsApplied()
        {
            SetupBasketToRejectPromotion();

            _promotion.ApplyPromotionIfApplicable(_basket);

            _basket.BasketDetail.PromotionsApplied.Should().BeEmpty();
        }

        [Test]
        public void IfTheBasketIsValidForThePromotionThePromotionIsApplied()
        {
            SetupBasketToApplyPromotion(1,1);

            _promotion.ApplyPromotionIfApplicable(_basket);

            _basket.BasketDetail.PromotionsApplied.Should().Contain(_promotion.Description);
        }

        [Test]
        public void IfMoreOfSkuCIsOrderedThanSkuDTheBasketIsUpdatedAccordingly()
        {
            var productCQty = 2;
            var productDQty = 1;
            SetupBasketToApplyPromotion(productCQty,productDQty);
            var priceForProductC = _productsIndexedBySku['C'].Price;
            var surplusProductC = productCQty - productDQty;
            
            _promotion.ApplyPromotionIfApplicable(_basket);

            _basket.BasketLines.First(x => x.Product.Sku == 'C').LineTotal
                .Should().Be(surplusProductC * priceForProductC);
            _basket.BasketLines.First(x => x.Product.Sku == 'D').LineTotal
                .Should().Be(productDQty * 30); //30 is hardcoded promotional price
        }

        [Test]
        public void IfMoreOfSkuDIsOrderedThanSkuCTheBasketIsUpdatedAccordingly()
        {
            var productCQty = 1;
            var productDQty = 2;
            SetupBasketToApplyPromotion(productCQty,productDQty);
            var priceForProductD = _productsIndexedBySku['D'].Price;
            var surplusProductD = productDQty - productCQty;
            
            _promotion.ApplyPromotionIfApplicable(_basket);
            
            _basket.BasketLines.First(x => x.Product.Sku == 'C').LineTotal
                .Should().Be(productCQty * 30); //30 is hardcoded promotional price
            _basket.BasketLines.First(x => x.Product.Sku == 'D').LineTotal
                .Should().Be(surplusProductD * priceForProductD);
        }
        
        #region Supporting Code
        
        [SetUp]
        public void Setup()
        {
            _productsIndexedBySku = TestData.ProductsIndexedBySku;
            _basket = new Basket();
        }
        
        void SetupBasketToApplyPromotion(int qtyForSkuC, int qtyForSkuD)
        {
            _basket.BasketLines.Add(
               new()
            {
                Product = _productsIndexedBySku['C'],
                Quantity = qtyForSkuC,
                LineTotal = 0
            });
            
            _basket.BasketLines.Add(
                new()
                {
                    Product = _productsIndexedBySku['D'],
                    Quantity = qtyForSkuD,
                    LineTotal = 0
                });
        }

        void SetupBasketToRejectPromotion()
        {
            _basket.BasketLines.Add(new()
            {
                Product = _productsIndexedBySku['C'],
                Quantity = 0,
                LineTotal = 0
            });
            
            _basket.BasketLines.Add(new()
            {
                Product = _productsIndexedBySku['D'],
                Quantity = 0,
                LineTotal = 0
            });
        }
        
        #endregion
        
    }
}