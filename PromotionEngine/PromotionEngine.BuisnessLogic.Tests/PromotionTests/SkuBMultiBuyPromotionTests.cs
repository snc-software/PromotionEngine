using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using PromotionEngine.BuisnessLogic.Promotions;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.Domain;
using PromotionEngine.TestDataSet;
using ServiceStack;

namespace PromotionEngine.BuisnessLogic.Tests.PromotionTests
{
    [TestFixture]
    public class SkuBMultiBuyPromotionTests
    {
        readonly IPromotion _promotion = new SkuBMultiBuyPromotion();
        IDictionary<char, ProductModel> _productsIndexedBySku;
        Basket _basket;
        char ProductSku = 'B';

        [Test]
        public void IfTheBasketContentsAreNotValidForPromotionNoPromotionIsApplied()
        {
            SetupBasketToRejectPromotion();

            _promotion.ApplyPromotionIfApplicable(_basket);
            
            _basket.BasketDetail.PromotionsApplied.Should().BeEmpty();
        }

        [Test]
        public void IfTheBasketContentsAreNotValidForPromotionNoUpdatesAreMade()
        {
            SetupBasketToRejectPromotion();
            var previousBasket = _basket.CreateCopy();

            _promotion.ApplyPromotionIfApplicable(_basket);
            
            _basket.Should().BeEquivalentTo(previousBasket);
        }

        [Test]
        public void IfTheBasketContentsAreValidThePromotionIsApplied()
        {
            SetupBasketToApplyPromotion();
            
            _promotion.ApplyPromotionIfApplicable(_basket);

            _basket.BasketDetail.PromotionsApplied.Should().Contain(_promotion.Description);
        }
        
        [Test]
        public void IfTheBasketContentsAreValidTheLineTotalIsUpdatedForTheMultiBuySku()
        {
            SetupBasketToApplyPromotion();
            var otherBasketLines = _basket.CreateCopy().BasketLines.Where(x => x.Product.Sku
                                                                               != ProductSku);
            _promotion.ApplyPromotionIfApplicable(_basket);
            
            _basket.BasketLines.First(x => x.Product.Sku == ProductSku).LineTotal.Should().Be(45);
            _basket.BasketLines.Where(x => x.Product.Sku != ProductSku).Should().BeEquivalentTo(otherBasketLines);
        }

        #region Supporting Code

        [SetUp]
        public void Setup()
        {
            _productsIndexedBySku = TestData.ProductsIndexedBySku;
            _basket = new Basket();
        }

        void SetupBasketToApplyPromotion()
        {
            _basket.BasketLines.Add(new()
            {
                Product = _productsIndexedBySku[ProductSku],
                Quantity = 2,
                LineTotal = 0
            });
        }

        void SetupBasketToRejectPromotion()
        {
            _basket.BasketLines.Add(new()
            {
                Product = _productsIndexedBySku[ProductSku],
                Quantity = 1,
                LineTotal = 0
            });
        }
        
        #endregion
    }
}