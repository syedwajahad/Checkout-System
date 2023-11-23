using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using offers.Business.Implementation;
using offers.Business.Interface;
using offers.DataObjects.DataModels;
using OffersApi.Controllers;
using System.Net;
using System.Security.Claims;

namespace OffersApiTests
{
    public class OffersApiControllersShould : ControllerBase
    {
        private readonly IOfferBusiness _offersBusiness;
        private readonly OfferController _offersController;

        public OffersApiControllersShould()
        {         
            _offersBusiness = Substitute.For<IOfferBusiness>();         
            _offersController = new OfferController(_offersBusiness);
        }
        

        [Fact]
        public async Task GetOffers_Returns_OkResult_With_OffersList()
        {
            // Arrange
            int productId = 1;
        List<Offer> mockOffers = new List<Offer>
        {
            new Offer {OfferId = 1, OfferName = "Offer 1", }
            
        };

            _offersBusiness.GetOffer(productId).Returns(Task.FromResult(mockOffers));

            // Act
            var result = await _offersController.GetOffers(productId);

            // Assert
            result.Should().BeOfType<ActionResult<List<Offer>>>();
            var actionResult = result.Result;
            actionResult.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)actionResult;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().BeEquivalentTo(mockOffers);
        }
        [Fact]
        public async Task GetOffers_WithInvalidProductId_ShouldReturnBadRequest()
        {
            // Arrange
            int invalidProductId = 0; // Invalid ProductId
            _offersBusiness.GetOffer(invalidProductId).Returns((List<Offer>)null);

            // Act
            var result = await _offersController.GetOffers(invalidProductId);

            // Assert
            result.Should().BeOfType<ActionResult<List<Offer>>>();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = (BadRequestObjectResult)result.Result;
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            badRequestResult.Value.Should().Be("No users found.");
        }
        [Fact]
        public async Task DeleteOffers_WithValidUserId_ShouldReturnOk()
        {
            // Arrange
            int validUserId = 1;
            _offersBusiness.DeleteOffer(validUserId).Returns(1); 

            // Act
            var result = await _offersController.deleteOffers(validUserId);

            // Assert
            result.Should().BeOfType<ActionResult<Offer>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result.Result;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().Be("deleted successfully");
        }
        [Fact]
        public async Task DeleteOffers_WithNonExistingUserId_ShouldReturnBadRequest()
        {
            // Arrange
            int nonExistingUserId = 100; 
            _offersBusiness.DeleteOffer(nonExistingUserId).Returns(0); 
 
            // Act
            var result = await _offersController.deleteOffers(nonExistingUserId);
            
            // Assert
            result.Should().BeOfType<ActionResult<Offer>>();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = (BadRequestObjectResult)result.Result;
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            badRequestResult.Value.Should().Be("No offers found.");
        }
       


    }
}