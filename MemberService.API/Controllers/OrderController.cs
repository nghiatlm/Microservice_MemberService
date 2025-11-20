using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Requests;
using MemberService.BO.Responses;
using MemberService.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.API.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        // [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var result = await _orderService.Create(request);
            return result != null ? Ok(ApiResponse<string>.SuccessResponse(result, "Order created")) : BadRequest(ApiResponse<object>.BadRequest("Creation failed"));
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null) return NotFound(ApiResponse<object>.NotFound("Order not found"));
            return Ok(ApiResponse<Order>.SuccessResponse(order, "Fetch successful"));
        }

        [HttpGet]
        // [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> GetOrders([
            FromQuery] string? query, [FromQuery] int? accountId = default,
            [FromQuery] int? packageId = default, [FromQuery] int? orderStatus = default,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _orderService.GetOrders(query, accountId, packageId, orderStatus, pageNumber, pageSize);
            return Ok(ApiResponse<PageResult<Order>>.SuccessResponse(result, "Fetch successful"));
        }
    }
}
