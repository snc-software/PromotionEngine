using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PromotionEngine.BuisnessLogic.Promotions.Interfaces;
using PromotionEngine.BuisnessLogic.Services;
using PromotionEngine.BuisnessLogic.Services.Interfaces;
using PromotionEngine.Domain;

namespace PromotionEngine.BuisnessLogic.Tests.ServicesTests
{
    public class PromotionApplierTests
    {
        Mock<IPromotion> _mockPromotion1;
        Mock<IPromotion> _mockPromotion2; 
        IList<IPromotion> _promotions;
        Basket _basket;
        IPromotionApplier _promotionApplier;
        
        [Test]
        public void ApplyingPromotionsTriesToApplyAllPromotions()
        {
            _promotionApplier.ApplyPromotions(_basket);

            _mockPromotion1.Verify(m => m.ApplyPromotionIfApplicable(It.IsAny<Basket>()));
            _mockPromotion2.Verify(m => m.ApplyPromotionIfApplicable(It.IsAny<Basket>()));
        }
                
        #region Supporting Code

        [SetUp]
        public void Setup()
        {
            SetupMocks();
            SetupData();
            SetupServiceUnderTest();
        }

        void SetupMocks()
        {
            _mockPromotion1 = new Mock<IPromotion>();
            _mockPromotion2 = new Mock<IPromotion>();
        }
        
        void SetupData()
        {
            _promotions = new List<IPromotion>
            {
                _mockPromotion1.Object,
                _mockPromotion2.Object
            };

            _basket = new Basket();
        }
        
        void SetupServiceUnderTest()
        {
            _promotionApplier = new PromotionApplier(_promotions);
        }

        #endregion
    }
}