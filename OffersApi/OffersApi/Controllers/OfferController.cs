using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using offers.Business.Interface;
using offers.DataObjects.DataModels;
using Offers.DataObjects.DataModels;
using System.Net;

namespace OffersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OfferController : ControllerBase
    {
        private readonly IOfferBusiness _OfferBusiness;
        public OfferController(IOfferBusiness OfferBusiness)
        {
            _OfferBusiness = OfferBusiness;
        }
        [HttpGet]
        [Route("{ProductId}")]
        [ProducesResponseType(typeof(IList<Offer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetOffers(int ProductId)
        {
            try
            {
                var response = await _OfferBusiness.GetOffer(ProductId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{OfferId}")]
        [ProducesResponseType(typeof(IList<Offer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteOffers(int OfferId)
        {
            try
            {
                var response = await _OfferBusiness.DeleteOffer(OfferId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}