using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Responses;
using MemberService.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.API.Controllers
{
    [ApiController]
    [Route("api/v1/members")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _memberService;

        public MemberController(ILogger<MemberController> logger, IMemberService memberService)
        {
            _logger = logger;
            _memberService = memberService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var member = await _memberService.GetById(id);
            return Ok(ApiResponse<Membership>.SuccessResponse(member, "Fetch successful"));
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers([FromQuery] int? accountId = default, [FromQuery] int? packageId = default, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _memberService.GetMembers(accountId, packageId, pageNumber, pageSize);
            return Ok(ApiResponse<PageResult<Membership>>.SuccessResponse(result, "Fetch successful"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Membership request)
        {
            var updated = await _memberService.Update(request);
            return updated > 0 ? Ok(ApiResponse<object>.SuccessResponse(null, "Updated")) : BadRequest(ApiResponse<object>.BadRequest("Update failed"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _memberService.Delete(id);
            return deleted > 0 ? Ok(ApiResponse<object>.SuccessResponse(null, "Deleted")) : BadRequest(ApiResponse<object>.BadRequest("Delete failed"));
        }
    }
}
