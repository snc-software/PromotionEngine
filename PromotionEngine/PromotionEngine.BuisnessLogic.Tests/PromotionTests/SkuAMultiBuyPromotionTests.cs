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
    public class SkuAMultiBuyPromotionTests
    {
        readonly IPromotion _promotion = new SkuAMultiBuyPromotion();
        IDictionary<char, ProductModel> _productsIndexedBySku;
        Basket _basket;

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
        public void IfTheBasketContentsAreValidTheLineTotalIsUpdatedForTheMultibuySku()
        {
            SetupBasketToApplyPromotion();
            var otherBasketLines = _basket.CreateCopy().BasketLines.Where(x => x.Product.Sku
                                                                               != 'A');
            _promotion.ApplyPromotionIfApplicable(_basket);
            
            _basket.BasketLines.First(x => x.Product.Sku == 'A').LineTotal.Should().Be(130);
            _basket.BasketLines.Where(x => x.Product.Sku != 'A').Should().BeEquivalentTo(otherBasketLines);
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
                Product = _productsIndexedBySku['A'],
                Quantity = 3,
                LineTotal = 0
            });
        }

        void SetupBasketToRejectPromotion()
        {
            _basket.BasketLines.Add(new()
            {
                Product = _productsIndexedBySku['A'],
                Quantity = 2,
                LineTotal = 0
            });
        }
        
        #endregion
    }
}