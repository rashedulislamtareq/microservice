using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetDiscount/{productName}")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _repository.GetDiscount(productName);
            return Ok(coupon);
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var response = await _repository.CreateDiscount(coupon);
            return Ok(coupon);
        }

        [HttpPut]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            await _repository.UpdateDiscount(coupon);
            return Ok(coupon);
        }

        [HttpDelete("DeleteDiscount/{productName}")]
        public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
        {
            var response = await _repository.DeleteDiscount(productName);
            return Ok(response);
        }
    }
}