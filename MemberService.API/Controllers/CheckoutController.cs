using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberService.BO.Responses;
using MemberService.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;

namespace MemberService.API.Controllers
{
    [ApiController]
    [Route("api/v1/checkouts")]
    public class CheckoutController : ControllerBase
    {
        private readonly IPayosService _payosService;

        public CheckoutController(IPayosService payosService)
        {
            _payosService = payosService;
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromBody] WebhookType type)
        {
            var resullt = await _payosService.VerifyPaymentAsync(type);
            if (resullt)
            {
                return Ok(ApiResponse<string>.SuccessResponse(null, "Payment verified successfully."));
            }
            else
            {
                return BadRequest(ApiResponse<string>.BadRequest("Payment verification failed."));
            }
        }
    }
}