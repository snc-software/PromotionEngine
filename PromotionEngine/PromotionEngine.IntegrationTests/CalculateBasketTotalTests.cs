using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PromotionEngine.Api.Controllers;
using PromotionEngine.ServiceInterface;
using PromotionEngine.TestDataSet;

namespace PromotionEngine.IntegrationTests
{
    [TestFixture]
    public class CalculateBasketTotalTests : IntegrationTestBase
    {
        CalculateBasketTotalController _controller;

        [Test]
        public async Task RunningAScenarioWithNoPromotionsUpdatesTheBasketAccordingly()
        {
            var scenario = TestData.Scenario1;
            
            var response = await _controller.Calculate(new CalculateBasketTotalRequest(scenario)) as OkObjectResult;

            response.Should().NotBeNull();
            response.Value.Should().BeOfType<CalculateBasketTotalResponse>();
            var calculateBasketTotalResponse = (CalculateBasketTotalResponse)response.Value;
            calculateBasketTotalResponse.Basket.BasketDetail.PromotionsApplied.Should().BeEmpty();
            calculateBasketTotalResponse.Basket.BasketDetail.Total.Should().Be(100);
        }
        
        
        [Test]
        public async Task RunningAScenarioWithMultiplePromotionsUpdatesTheBasketAccordingly()
        {
            var scenario = TestData.Scenario2;
            
            var response = await _controller.Calculate(new CalculateBasketTotalRequest(scenario)) as OkObjectResult;

            response.Should().NotBeNull();
            response.Value.Should().BeOfType<CalculateBasketTotalResponse>();
            var calculateBasketTotalResponse = (CalculateBasketTotalResponse)response.Value;
            calculateBasketTotalResponse.Basket.BasketDetail.PromotionsApplied.Should().BeEquivalentTo(
                new List<string> {"3 of A's for 130","2 of B's for 45"});
            calculateBasketTotalResponse.Basket.BasketLines.First(x => x.Product.Sku == 'A')
                .LineTotal.Should().Be(230);
            calculateBasketTotalResponse.Basket.BasketLines.First(x => x.Product.Sku == 'B')
                .LineTotal.Should().Be(120);
            calculateBasketTotalResponse.Basket.BasketDetail.Total.Should().Be(370);
        }
        
        
        [Test]
        public async Task RunningAScenarioWithAllValidPromotionsUpdatesTheBasketAccordingly()
        {
            var scenario = TestData.Scenario3;
            
            var response = await _controller.Calculate(new CalculateBasketTotalRequest(scenario)) as OkObjectResult;

            response.Should().NotBeNull();
            response.Value.Should().BeOfType<CalculateBasketTotalResponse>();
            var calculateBasketTotalResponse = (CalculateBasketTotalResponse)response.Value;
            calculateBasketTotalResponse.Basket.BasketDetail.PromotionsApplied.Should().BeEquivalentTo(
                new List<string> {"3 of A's for 130","2 of B's for 45", "C & D For 30"});
            calculateBasketTotalResponse.Basket.BasketLines.First(x => x.Product.Sku == 'A')
                .LineTotal.Should().Be(130);
            calculateBasketTotalResponse.Basket.BasketLines.First(x => x.Product.Sku == 'B')
                .LineTotal.Should().Be(120);
            calculateBasketTotalResponse.Basket.BasketLines.First(x => x.Product.Sku == 'C')
                .LineTotal.Should().Be(0);
            calculateBasketTotalResponse.Basket.BasketLines.First(x => x.Product.Sku == 'D')
                .LineTotal.Should().Be(30);
            calculateBasketTotalResponse.Basket.BasketDetail.Total.Should().Be(280);
        }

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _controller = new CalculateBasketTotalController(ServiceProvider.GetRequiredService<IMediator>());
        }
    }
}