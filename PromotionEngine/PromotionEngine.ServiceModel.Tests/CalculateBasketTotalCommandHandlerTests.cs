using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using PromotionEngine.BuisnessLogic.Services.Interfaces;
using PromotionEngine.Domain;
using PromotionEngine.ServiceModel.Handlers;
using PromotionEngine.ServiceModel.Requests;
using PromotionEngine.ServiceModel.Responses;
using PromotionEngine.TestDataSet;
using ServiceStack;

namespace PromotionEngine.ServiceModel.Tests
{
    [TestFixture]
    public class CalculateBasketTotalCommandHandlerTests
    {
        Mock<IPromotionApplier> _mockPromotionApplier;
        CalculateBasketTotalCommand _request;
        Basket _updatedBasket;
        IRequestHandler<CalculateBasketTotalCommand, CalculateBasketTotalCommandResponse> _requestHandler;

        [Test]
        public void HandlingTheCalculateBasketTotalCommandCallsThePromotionApplier()
        {
            _requestHandler.Handle(_request, default);
            
            _mockPromotionApplier.Verify(m => m.ApplyPromotions(_request.Basket), Times.Once);
        }
        
        [Test]
        public void HandlingTheCalculateBasketTotalCommandSetsTheTotalPriceInTheBasketDetails()
        {
            var expectedTotal = _updatedBasket.BasketLines.Sum(x => x.LineTotal);
            
            _requestHandler.Handle(_request, default);
            
            _updatedBasket.BasketDetail.Total.Should().Be(expectedTotal);
        }
        
        #region Supporting Code

        [SetUp]
        public void Setup()
        {
            SetupData();
            SetupMocks();
            SetupExpectations();
            SetupServiceUnderTest();
        }

        void SetupData()
        {
            var productsIndexedBySku = TestData.ProductsIndexedBySku;

            _request = new CalculateBasketTotalCommand
            {
                Basket = new Basket
                {
                    BasketDetail = new BasketDetailModel(),
                    BasketLines = new List<BasketLineModel>
                    {
                        new()
                        {
                            Product = productsIndexedBySku['A'],
                            Quantity = 12
                        },
                        new()
                        {
                            Product = productsIndexedBySku['B'],
                            Quantity = 1
                        }
                    }
                }
            };
                
            
            _updatedBasket = _request.Basket.CreateCopy();
            _updatedBasket.BasketDetail.PromotionsApplied.Add("3 A's for 130");
            _updatedBasket.BasketLines.First(x => x.Product.Sku == 'A').LineTotal = 4 * 130;
            _updatedBasket.BasketLines.First(x => x.Product.Sku == 'B').LineTotal = productsIndexedBySku['B'].Price;
        }
        
        void SetupMocks()
        {
            _mockPromotionApplier = new Mock<IPromotionApplier>();
        }

        void SetupExpectations()
        {
            _mockPromotionApplier
                .Setup(m => m.ApplyPromotions(_request.Basket))
                .Returns(_updatedBasket);
        }

        void SetupServiceUnderTest()
        {
            _requestHandler = new CalculateBasketTotalCommandHandler(_mockPromotionApplier.Object);
        }

        #endregion
    }
}