using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Responses;
using MemberService.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.API.Controllers
{
    [ApiController]
    [Route("api/v1/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var payment = await _paymentService.GetById(id);
            return Ok(ApiResponse<Payment>.SuccessResponse(payment, "Fetch successful"));
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments([FromQuery] int? orderId = default, [FromQuery] PaymentStatus? paymentStatus = default, [FromQuery] PaymentMethod? method = default, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _paymentService.GetPayments(orderId, paymentStatus, method, pageNumber, pageSize);
            return Ok(ApiResponse<PageResult<Payment>>.SuccessResponse(result, "Fetch successful"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Payment request)
        {
            var updated = await _paymentService.Update(request);
            return updated > 0 ? Ok(ApiResponse<object>.SuccessResponse(null, "Updated")) : BadRequest(ApiResponse<object>.BadRequest("Update failed"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _paymentService.Delete(id);
            return deleted > 0 ? Ok(ApiResponse<object>.SuccessResponse(null, "Deleted")) : BadRequest(ApiResponse<object>.BadRequest("Delete failed"));
        }
    }
}
